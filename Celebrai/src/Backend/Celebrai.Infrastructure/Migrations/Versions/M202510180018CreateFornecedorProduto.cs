using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202510180018, "Criação da tabela FornecedorProduto")]
[Profile("Celebrai")]
public class M202510180018CreateFornecedorProduto : Migration
{
    public override void Up()
    {
        Create.Table("FornecedorProduto")
            .WithColumn("IdFornecedor").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("IdProduto").AsInt32().NotNullable().PrimaryKey();

        Create.ForeignKey("FK_FornProd_Fornecedor")
            .FromTable("FornecedorProduto").ForeignColumn("IdFornecedor")
            .ToTable("Fornecedor").PrimaryColumn("IdFornecedor");

        Create.ForeignKey("FK_FornProd_Produto")
            .FromTable("FornecedorProduto").ForeignColumn("IdProduto")
            .ToTable("Produto").PrimaryColumn("IdProduto");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_FornProd_Fornecedor").OnTable("FornecedorProduto");
        Delete.ForeignKey("FK_FornProd_Produto").OnTable("FornecedorProduto");
        Delete.Table("FornecedorProduto");
    }
}