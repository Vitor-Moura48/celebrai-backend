using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class ProdutoMap : BaseMap<Produto>
{
    public ProdutoMap() : base("Produto") { }

    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdProduto);
        builder.Property(x => x.IdProduto).UseIdentityColumn(1000, 10);

        builder.HasOne(x => x.SubCategoria) 
               .WithMany()
               .HasForeignKey(x => x.IdSubcategoria)
               .HasConstraintName("FK_Produto_Subcategoria");
    }
}
