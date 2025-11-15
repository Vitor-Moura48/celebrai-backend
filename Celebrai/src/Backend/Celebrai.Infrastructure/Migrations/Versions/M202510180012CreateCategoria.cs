using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180012, "Criação da tabela Categoria")]
[Profile("Celebrai")]
public class M202510180012CreateCategoria : Migration
{
    public override void Up()
    {
        Create.Table("Categoria")
            .WithColumn("IdCategoria").AsInt32().PrimaryKey()
            .WithColumn("Nome").AsAnsiString(20).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Categoria");
    }
}