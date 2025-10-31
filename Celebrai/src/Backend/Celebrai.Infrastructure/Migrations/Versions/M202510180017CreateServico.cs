using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180017, "Criação da tabela Servico")]
[Profile("Celebrai")]
public class M202510180017CreateServico : Migration
{
    public override void Up()
    {
        Create.Table("Servico")
            .WithColumn("IdServico").AsInt32().PrimaryKey().Identity()
            .WithColumn("IdProduto").AsInt32().NotNullable()
            .WithColumn("DuracaoEstimada").AsInt32().NotNullable();

        Create.ForeignKey("FK_Servico_Produto")
            .FromTable("Servico").ForeignColumn("IdProduto")
            .ToTable("Produto").PrimaryColumn("IdProduto");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Servico_Produto").OnTable("Servico");
        Delete.Table("Servico");
    }
}