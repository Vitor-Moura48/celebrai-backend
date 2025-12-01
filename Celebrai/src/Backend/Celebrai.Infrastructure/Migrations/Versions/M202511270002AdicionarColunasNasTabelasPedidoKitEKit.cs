using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511270002, "Adição das Colunas nas Tabelas PedidoKit e Kit")]
[Profile("Celebrai")]
public class M202511270002AdicionarColunasNasTabelasPedidoKitEKit : Migration
{
    public override void Up()
    {
        if (Schema.Table("Kit").Column("QuantidadeAluguelPorDia").Exists() == false &&
            Schema.Table("Kit").Column("Nome").Exists() == false &&
            Schema.Table("Kit").Column("Descricao").Exists() == false)
        {
            Alter.Table("Kit")
                .AddColumn("QuantidadeAluguelPorDia").AsInt32().NotNullable()
                .AddColumn("Nome").AsAnsiString(80).NotNullable()
                .AddColumn("Descricao").AsAnsiString(400).NotNullable();
        }

        if (Schema.Table("PedidoKit").Column("Data").Exists() == false)
        {
            Alter.Table("PedidoKit")
                .AddColumn("Data").AsDate().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("Kit").Column("QuantidadeAluguelPorDia").Exists() && 
            Schema.Table("Kit").Column("Descricao").Exists() && 
            Schema.Table("Kit").Column("Nome").Exists())
        {
            Delete.Column("QuantidadeAluguelPorDia").FromTable("Kit");
            Delete.Column("Descricao").FromTable("Kit");
            Delete.Column("Nome").FromTable("Kit");
        }

        if (Schema.Table("PedidoKit").Column("Data").Exists())
        {
            Delete.Column("Data").FromTable("PedidoKit");
        }
    }
}
