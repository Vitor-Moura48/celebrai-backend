using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class DisponibilidadeMap : BaseMap<Disponibilidade>
{
    public DisponibilidadeMap() : base("Disponibilidade") { }

    public override void Configure(EntityTypeBuilder<Disponibilidade> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdDisponibilidade);
        builder.Property(x => x.IdDisponibilidade).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_Disponibilidade_Fornecedor");
    }
}
