using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180010, "Criação da tabela Cartao")]
[Profile("Celebrai")]
public class M202510180010CreateCartao : Migration
{
    public override void Up()
    {
        Create.Table("Cartao")
            .WithColumn("IdCartao").AsInt32().PrimaryKey().Identity()
            .WithColumn("IdPagamento").AsInt32().NotNullable()
            .WithColumn("TipoCartao").AsFixedLengthAnsiString(1).NotNullable()
            .WithColumn("AutorizacaoTid").AsAnsiString(20).NotNullable().Unique()
            .WithColumn("Parcelas").AsInt32().NotNullable()
            .WithColumn("NumeroCartao").AsFixedLengthAnsiString(4).NotNullable()
            .WithColumn("DataValidade").AsDate().NotNullable()
            .WithColumn("Titular").AsAnsiString(50).NotNullable()
            .WithColumn("Bandeira").AsAnsiString(20).NotNullable();

        Create.ForeignKey("FK_Cartao_Pagamento")
            .FromTable("Cartao").ForeignColumn("IdPagamento")
            .ToTable("Pagamento").PrimaryColumn("IdPagamento");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_Cartao_Pagamento").OnTable("Cartao");
        Delete.Table("Cartao");
    }
}