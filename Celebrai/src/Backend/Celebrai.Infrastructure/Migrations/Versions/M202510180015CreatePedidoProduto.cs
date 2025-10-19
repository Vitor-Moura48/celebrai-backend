using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180015, "Criação da tabela PedidoProduto")]
[Profile("Celebrai")]
public class M202510180015CreatePedidoProduto : Migration
{
    public override void Up()
    {
        Create.Table("PedidoProduto")
            .WithColumn("IdPedido").AsInt32().NotNullable().PrimaryKey()
            .WithColumn("IdProduto").AsInt32().NotNullable().PrimaryKey()
            .WithColumn("Quantidade").AsInt32().NotNullable()
            .WithColumn("Preco").AsCurrency().NotNullable()
            .WithColumn("Avaliacao").AsAnsiString(250).Nullable()
            .WithColumn("Nota").AsInt32().Nullable();

        Create.ForeignKey("FK_PedidoProduto_Pedido")
            .FromTable("PedidoProduto").ForeignColumn("IdPedido")
            .ToTable("Pedido").PrimaryColumn("IdPedido");

        Create.ForeignKey("FK_PedidoProduto_Produto")
            .FromTable("PedidoProduto").ForeignColumn("IdProduto")
            .ToTable("Produto").PrimaryColumn("IdProduto");

        Execute.Sql("ALTER TABLE PedidoProduto ADD CONSTRAINT CK_PedidoProduto_Quantidade CHECK (Quantidade > 0)");
        Execute.Sql("ALTER TABLE PedidoProduto ADD CONSTRAINT CK_PedidoProduto_Preco CHECK (Preco > 0)");
        Execute.Sql("ALTER TABLE PedidoProduto ADD CONSTRAINT CK_PedidoProduto_Nota CHECK (Nota BETWEEN 1 AND 5)");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE PedidoProduto DROP CONSTRAINT CK_PedidoProduto_Quantidade");
        Execute.Sql("ALTER TABLE PedidoProduto DROP CONSTRAINT CK_PedidoProduto_Preco");
        Execute.Sql("ALTER TABLE PedidoProduto DROP CONSTRAINT CK_PedidoProduto_Nota");

        Delete.ForeignKey("FK_PedidoProduto_Pedido").OnTable("PedidoProduto");
        Delete.ForeignKey("FK_PedidoProduto_Produto").OnTable("PedidoProduto");

        Delete.Table("PedidoProduto");
    }
}