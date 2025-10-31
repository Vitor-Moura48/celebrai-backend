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

        // Chave Primária (PK) com Identity (seed 1000, increment 10)
        builder.HasKey(x => x.IdProduto);
        builder.Property(x => x.IdProduto).UseIdentityColumn(1000, 10);

        // Chave Estrangeira (FK)
        builder.HasOne(x => x.SubCategoria) // Assumindo propriedade de navegação
               .WithMany()
               .HasForeignKey(x => x.IdSubcategoria)
               .HasConstraintName("FK_Produto_Subcategoria");
    }
}
