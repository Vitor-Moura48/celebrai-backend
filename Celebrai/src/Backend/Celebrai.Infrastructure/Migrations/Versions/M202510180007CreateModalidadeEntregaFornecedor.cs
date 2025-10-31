using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180007, "Criação da tabela ModalidadeEntregaFornecedor")]
[Profile("Celebrai")]
public class M202510180007CreateModalidadeEntregaFornecedor : Migration
{
    public override void Up()
    {
        Create.Table("ModalidadeEntregaFornecedor")
            .WithColumn("IdModalidadeEntrega").AsInt32().NotNullable().PrimaryKey()
            .WithColumn("IdFornecedor").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("ValorBase").AsCurrency().NotNullable()
            .WithColumn("PrecoKm").AsCurrency().NotNullable();

        Create.ForeignKey("FK_ModEntregaForn_ModEntrega")
            .FromTable("ModalidadeEntregaFornecedor").ForeignColumn("IdModalidadeEntrega")
            .ToTable("ModalidadeEntrega").PrimaryColumn("IdModalidadeEntrega");

        Create.ForeignKey("FK_ModEntregaForn_Fornecedor")
            .FromTable("ModalidadeEntregaFornecedor").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");

        Execute.Sql("ALTER TABLE ModalidadeEntregaFornecedor ADD CONSTRAINT CK_ModEntregaForn_ValorBase CHECK (ValorBase > 0)");
        Execute.Sql("ALTER TABLE ModalidadeEntregaFornecedor ADD CONSTRAINT CK_ModEntregaForn_PrecoKm CHECK (PrecoKm > 0)");
    }

    public override void Down()
    {
        Execute.Sql("ALTER TABLE ModalidadeEntregaFornecedor DROP CONSTRAINT CK_ModEntregaForn_ValorBase");
        Execute.Sql("ALTER TABLE ModalidadeEntregaFornecedor DROP CONSTRAINT CK_ModEntregaForn_PrecoKm");

        Delete.ForeignKey("FK_ModEntregaForn_ModEntrega").OnTable("ModalidadeEntregaFornecedor");
        Delete.ForeignKey("FK_ModEntregaForn_Fornecedor").OnTable("ModalidadeEntregaFornecedor");

        Delete.Table("ModalidadeEntregaFornecedor");
    }
}