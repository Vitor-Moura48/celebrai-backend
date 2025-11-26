using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511260001, "Adição da Coluna PrecoUnitario")]
[Profile("Celebrai")]
public class M202511260001AdicionarColunaPrecoUnitario : Migration
{
    public override void Up()
    {
        Alter.Table("Produto")
            .AddColumn("PrecoUnitario").AsCurrency().NotNullable();
    }

    public override void Down()
    {
        if (Schema.Table("Produto").Column("PrecoUnitario").Exists())
        {
            Delete.Column("PrecoUnitario").FromTable("Produto");
        }
    }
}
