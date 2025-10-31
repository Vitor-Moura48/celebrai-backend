using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class FornecedorProdutoMap : BaseMap<FornecedorProduto>
{
    public FornecedorProdutoMap() : base("FornecedorProduto") { }

    public override void Configure(EntityTypeBuilder<FornecedorProduto> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.IdFornecedor, x.IdProduto });

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_FornProd_Fornecedor");

        builder.HasOne(x => x.Produto)
               .WithMany()
               .HasForeignKey(x => x.IdProduto)
               .HasConstraintName("FK_FornProd_Produto");
    }
}
