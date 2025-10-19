using FluentMigrator;
using System.Data;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180013, "Criação da tabela SubCategoria")]
[Profile("Celebrai")]
public class M202510180013CreateSubCategoria : Migration
{
    public override void Up()
    {
        Create.Table("SubCategoria")
            .WithColumn("IdSubCategoria").AsInt32().PrimaryKey().Identity()
            .WithColumn("IdCategoria").AsInt32().NotNullable()
            .WithColumn("Nome").AsAnsiString(20).NotNullable();

        Create.ForeignKey("FK_SubCategoria_Categoria")
            .FromTable("SubCategoria").ForeignColumn("IdCategoria")
            .ToTable("Categoria").PrimaryColumn("IdCategoria")
            .OnDelete(Rule.Cascade); 
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_SubCategoria_Categoria").OnTable("SubCategoria");
        Delete.Table("SubCategoria");
    }
}