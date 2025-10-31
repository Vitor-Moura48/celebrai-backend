using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PixMap : BaseMap<Pix>
{
    public PixMap() : base("Pix") { }

    public override void Configure(EntityTypeBuilder<Pix> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdPix);
        builder.Property(x => x.IdPix).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Pagamento)
               .WithMany()
               .HasForeignKey(x => x.IdPagamento)
               .HasConstraintName("FK_Pix_Pagamento");
    }
}
