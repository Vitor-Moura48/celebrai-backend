using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class MaterialMap : BaseMap<Material>
{
    public MaterialMap() : base("Material") { }

    public override void Configure(EntityTypeBuilder<Material> builder)
    {
        base.Configure(builder);

        // Chave Primária (PK) com Identity
        builder.HasKey(x => x.IdMaterial);
        builder.Property(x => x.IdMaterial).ValueGeneratedOnAdd();

        // Chave Estrangeira (FK)
        builder.HasOne(x => x.Produto)
               .WithMany()
               .HasForeignKey(x => x.IdProduto)
               .HasConstraintName("FK_Material_Produto");
    }
}
