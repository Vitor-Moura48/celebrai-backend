using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511270001, "Adição das Colunas nas Tabelas PedidoProduto e Produto")]
[Profile("Celebrai")]
public class M202511270001AdicionarColunasNasTabelasPedidoProdutoEProduto : Migration
{
    public override void Up()
    {
        if (Schema.Table("Produto").Column("QuantidadeAluguelPorDia").Exists() == false)
        {
            Alter.Table("Produto")
                .AddColumn("QuantidadeAluguelPorDia").AsInt32().NotNullable();
        }

        if (Schema.Table("PedidoProduto").Column("Data").Exists() == false)
        {
            Alter.Table("PedidoProduto")
                .AddColumn("Data").AsDate().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("Produto").Column("QuantidadeAluguelPorDia").Exists())
        {
            Delete.Column("QuantidadeAluguelPorDia").FromTable("Produto");
        }

        if (Schema.Table("PedidoProduto").Column("Data").Exists())
        {
            Delete.Column("Data").FromTable("PedidoProduto");
        }
    }
}
