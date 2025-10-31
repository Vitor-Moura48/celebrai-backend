using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class SubCategoriaMap : BaseMap<SubCategoria>
{
    public SubCategoriaMap() : base("SubCategoria") { }

    public override void Configure(EntityTypeBuilder<SubCategoria> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdSubCategoria);
        builder.Property(x => x.IdSubCategoria).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Categoria)
               .WithMany()
               .HasForeignKey(x => x.IdCategoria)
               .HasConstraintName("FK_SubCategoria_Categoria")
               .OnDelete(DeleteBehavior.Cascade); 
    }
}
