using FluentMigrator;

namespace Celebrai.Infrastructure.Migrations.Versions;

[Migration(202511100001, "Adição das tabelas Kit e ProdutoKit e deletar a tabela Material")]
[Profile("Celebrai")]
public class M202511100001AdicionarTabelasKitEProdutoKitEDeletarTabelaMaterial : ForwardOnlyMigration
{
    public override void Up()
    {
        Delete.Table("Material");

        Create.Table("Kit")
            .WithColumn("IdKit").AsInt32().PrimaryKey().Identity()
            .WithColumn("VendaIndividual").AsBoolean().NotNullable();

        Create.Table("ProdutoKit")
            .WithColumn("IdProduto").AsInt32().NotNullable()
            .WithColumn("IdKit").AsInt32().NotNullable();

        Create.PrimaryKey("PK_ProdutoKit")
                .OnTable("ProdutoKit")
                .Columns("IdProduto", "IdKit");

        Create.ForeignKey("FK_ProdutoKit_Produto")
                .FromTable("ProdutoKit").ForeignColumn("IdProduto")
                .ToTable("Produto").PrimaryColumn("IdProduto")
                .OnDelete(System.Data.Rule.Cascade); 

        Create.ForeignKey("FK_ProdutoKit_Kit")
            .FromTable("ProdutoKit").ForeignColumn("IdKit")
            .ToTable("Kit").PrimaryColumn("IdKit")
            .OnDelete(System.Data.Rule.Cascade);
    }
}
