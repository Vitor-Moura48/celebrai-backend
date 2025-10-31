using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180016, "Criação da tabela Material")]
[Profile("Celebrai")]
public class M202510180016CreateMaterial : Migration
{
    public override void Up()
    {
        Create.Table("Material")
            .WithColumn("IdMaterial").AsInt32().PrimaryKey().Identity()
            .WithColumn("IdProduto").AsInt32().NotNullable()
            .WithColumn("VendaIndividual").AsBoolean().NotNullable();

        Create.ForeignKey("FK_Material_Produto")
            .FromTable("Material").ForeignColumn("IdProduto")
            .ToTable("Produto").PrimaryColumn("IdProduto");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Material_Produto").OnTable("Material");
        Delete.Table("Material");
    }
}