using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class CategoriaMap : BaseMap<Categoria>
{
    public CategoriaMap() : base("Categoria") { }

    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdCategoria);
        builder.Property(x => x.IdCategoria).ValueGeneratedOnAdd();
    }
}
