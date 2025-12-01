using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class ProdutoKitMap : BaseMap<ProdutoKit>
{
    public ProdutoKitMap() : base("ProdutoKit")
    {
    }

    public override void Configure(EntityTypeBuilder<ProdutoKit> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.IdKit, x.IdProduto });

        builder.HasOne(x => x.Kit)
               .WithMany(k => k.ProdutosKit)
               .HasForeignKey(x => x.IdKit)
               .HasConstraintName("FK_ProdKit_Kit");

        builder.HasOne(x => x.Produto)
               .WithMany()
               .HasForeignKey(x => x.IdProduto)
               .HasConstraintName("FK_ProdKit_Produto");
    }
}
