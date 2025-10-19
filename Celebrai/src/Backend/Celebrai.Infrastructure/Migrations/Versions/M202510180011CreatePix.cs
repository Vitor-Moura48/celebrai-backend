using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180011, "Criação da tabela Pix")]
[Profile("Celebrai")]
public class M202510180011CreatePix : Migration
{
    public override void Up()
    {
        Create.Table("Pix")
            .WithColumn("IdPix").AsInt32().PrimaryKey().Identity()
            .WithColumn("IdPagamento").AsInt32().NotNullable()
            .WithColumn("Txid").AsAnsiString(36).NotNullable().Unique()
            .WithColumn("InstituicaoFinanceira").AsAnsiString(50).NotNullable()
            .WithColumn("ChavePix").AsAnsiString(50).NotNullable();

        Create.ForeignKey("FK_Pix_Pagamento")
            .FromTable("Pix").ForeignColumn("IdPagamento")
            .ToTable("Pagamento").PrimaryColumn("IdPagamento");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Pix_Pagamento").OnTable("Pix");
        Delete.Table("Pix");
    }
}