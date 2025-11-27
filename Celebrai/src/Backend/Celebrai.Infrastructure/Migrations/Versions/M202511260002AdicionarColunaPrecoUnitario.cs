using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511260002, "Adição da Coluna PrecoUnitario")]
[Profile("Celebrai")]
public class M202511260002AdicionarColunaPrecoUnitario : Migration
{
    public override void Up()
    {
        if (Schema.Table("Produto").Column("PrecoUnitario").Exists() == false)
        {
            Alter.Table("Produto")
                .AddColumn("PrecoUnitario").AsCurrency().NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table("Produto").Column("PrecoUnitario").Exists())
        {
            Delete.Column("PrecoUnitario").FromTable("Produto");
        }
    }
}
