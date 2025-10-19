using FluentMigrator;
using FluentMigrator.SqlServer;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180014, "Criação da tabela Produto")]
[Profile("Celebrai")]
public class M202510180014CreateProduto : Migration
{
    public override void Up()
    {
        Create.Table("Produto")
            .WithColumn("IdProduto").AsInt32().PrimaryKey().Identity(1000, 10)
            .WithColumn("IdSubcategoria").AsInt32().NotNullable()
            .WithColumn("Nome").AsAnsiString(80).NotNullable()
            .WithColumn("Descricao").AsAnsiString(400).NotNullable();

        Create.ForeignKey("FK_Produto_Subcategoria")
            .FromTable("Produto").ForeignColumn("IdSubcategoria")
            .ToTable("Subcategoria").PrimaryColumn("IdSubcategoria");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Produto_Subcategoria").OnTable("Produto");
        Delete.Table("Produto");
    }
}