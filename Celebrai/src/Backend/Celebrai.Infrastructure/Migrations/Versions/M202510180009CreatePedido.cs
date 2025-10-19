using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180009, "Criação da tabela Pedido")]
[Profile("Celebrai")]
public class M202510180009CreatePedido : Migration
{
    public override void Up()
    {
        Create.Table("Pedido")
            .WithColumn("IdPedido").AsInt32().PrimaryKey().Identity(1000, 10)
            .WithColumn("IdModalidadeEntrega").AsInt32().NotNullable()
            .WithColumn("IdUsuario").AsGuid().NotNullable()
            .WithColumn("IdFornecedor").AsGuid().NotNullable()
            .WithColumn("IdPagamento").AsInt32().NotNullable()
            .WithColumn("ValorTotal").AsCurrency().NotNullable()
            .WithColumn("ValorFrete").AsCurrency().NotNullable()
            .WithColumn("DataPedido").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("Status").AsFixedLengthAnsiString(1).NotNullable();

        Create.ForeignKey("FK_Pedido_Usuario")
            .FromTable("Pedido").ForeignColumn("IdUsuario")
            .ToTable("Usuario").PrimaryColumn("IdUsuario");

        Create.ForeignKey("FK_Pedido_Fornecedor")
            .FromTable("Pedido").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");

        Create.ForeignKey("FK_Pedido_ModalidadeEntrega")
            .FromTable("Pedido").ForeignColumn("IdModalidadeEntrega")
            .ToTable("ModalidadeEntrega").PrimaryColumn("IdModalidadeEntrega");

        Create.ForeignKey("FK_Pedido_Pagamento")
            .FromTable("Pedido").ForeignColumn("IdPagamento")
            .ToTable("Pagamento").PrimaryColumn("IdPagamento");

        Execute.Sql("ALTER TABLE Pedido ADD CONSTRAINT CK_Pedido_ValorTotal CHECK (ValorTotal > 0)");
        Execute.Sql("ALTER TABLE Pedido ADD CONSTRAINT CK_Pedido_ValorFrete CHECK (ValorFrete >= 0)");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE Pedido DROP CONSTRAINT CK_Pedido_ValorTotal");
        Execute.Sql("ALTER TABLE Pedido DROP CONSTRAINT CK_Pedido_ValorFrete");

        Delete.ForeignKey("FK_Pedido_Usuario").OnTable("Pedido");
        Delete.ForeignKey("FK_Pedido_Fornecedor").OnTable("Pedido");
        Delete.ForeignKey("FK_Pedido_ModalidadeEntrega").OnTable("Pedido");
        Delete.ForeignKey("FK_Pedido_Pagamento").OnTable("Pedido");

        Delete.Table("Pedido");
    }
}