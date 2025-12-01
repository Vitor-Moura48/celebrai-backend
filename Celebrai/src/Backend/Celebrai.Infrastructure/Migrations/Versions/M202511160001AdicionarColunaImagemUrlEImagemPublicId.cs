using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511160001, "Adição das colubas ImagemUrl e ImagemPublicId")]
[Profile("Celebrai")]
public class M202511160001AdicionarColunaImagemUrlEImagemPublicId : Migration
{
    public override void Up()
    {
        Alter.Table("Produto")
            .AddColumn("ImagemUrl")
            .AsString(500).Nullable()
            .AddColumn("ImagemPublicId")
            .AsString(300).Nullable();
    }

    public override void Down()
    {
        if (Schema.Table("Produto").Column("ImagemUrl").Exists() && Schema.Table("Produto").Column("ImagemPublicId").Exists())
        {
            Delete.Column("ImagemUrl").FromTable("Produto");
            Delete.Column("ImagemPublicId").FromTable("Produto");
        }
    }
}
