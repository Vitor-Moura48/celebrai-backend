using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class KitMap : BaseMap<Kit>
{
    public KitMap() : base("Kit")
    {
    }

    public override void Configure(EntityTypeBuilder<Kit> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdKit);
        builder.Property(x => x.IdKit).ValueGeneratedOnAdd();

        builder.Property(x => x.KitPreco).HasPrecision(18, 2);
    }
}
