using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511260001, "Criação da Tabela PedidoKit")]
[Profile("Celebrai")]
public class M202511260001CreateDaTabelaPedidoKit : Migration
{
    public override void Up()
    {
        Create.Table("PedidoKit")
            .WithColumn("IdPedido").AsInt32().NotNullable().PrimaryKey()
            .WithColumn("IdKit").AsInt32().NotNullable().PrimaryKey()
            .WithColumn("Quantidade").AsInt32().NotNullable()
            .WithColumn("Preco").AsCurrency().NotNullable()
            .WithColumn("Avaliacao").AsAnsiString(250).Nullable()
            .WithColumn("Nota").AsInt32().Nullable();

        Create.ForeignKey("FK_PedidoKit_Pedido")
            .FromTable("PedidoKit").ForeignColumn("IdPedido")
            .ToTable("Pedido").PrimaryColumn("IdPedido");

        Create.ForeignKey("FK_PedidoKit_Kit")
            .FromTable("PedidoKit").ForeignColumn("IdKit")
            .ToTable("Kit").PrimaryColumn("IdKit");

        Execute.Sql("ALTER TABLE PedidoKit ADD CONSTRAINT CK_PedidoKit_Quantidade CHECK (Quantidade > 0)");
        Execute.Sql("ALTER TABLE PedidoKit ADD CONSTRAINT CK_PedidoKit_Preco CHECK (Preco > 0)");
        Execute.Sql("ALTER TABLE PedidoKit ADD CONSTRAINT CK_PedidoKit_Nota CHECK (Nota BETWEEN 1 AND 5)");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE PedidoKit DROP CONSTRAINT CK_PedidoKit_Quantidade");
        Execute.Sql("ALTER TABLE PedidoKit DROP CONSTRAINT CK_PedidoKit_Preco");
        Execute.Sql("ALTER TABLE PedidoKit DROP CONSTRAINT CK_PedidoKit_Nota");

        Delete.ForeignKey("FK_PedidoKit_Pedido").OnTable("PedidoKit");
        Delete.ForeignKey("FK_PedidoKit_Kit").OnTable("PedidoKit");

        Delete.Table("PedidoKit");
    }
}
