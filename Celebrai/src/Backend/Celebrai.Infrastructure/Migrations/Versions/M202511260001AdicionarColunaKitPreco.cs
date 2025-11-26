using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511260001, "Adição da Coluna KitPreco")]
[Profile("Celebrai")]
public class M202511260001AdicionarColunaKitPreco : Migration
{
    public override void Up()
    {
        Alter.Table("Kit")
            .AddColumn("KitPreco").AsCurrency().NotNullable();
    }

    public override void Down()
    {
        if (Schema.Table("Kit").Column("KitPreco").Exists())
        {
            Delete.Column("KitPreco").FromTable("Kit");
        }
    }
}
