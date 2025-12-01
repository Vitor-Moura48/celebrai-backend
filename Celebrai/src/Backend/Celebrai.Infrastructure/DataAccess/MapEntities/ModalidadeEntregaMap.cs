using Celebrai.Domain.Entities;
using Celebrai.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class ModalidadeEntregaMap : BaseMap<ModalidadeEntrega>
{
    public ModalidadeEntregaMap() : base("ModalidadeEntrega") { }

    public override void Configure(EntityTypeBuilder<ModalidadeEntrega> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdModalidadeEntrega);
        builder.Property(x => x.IdModalidadeEntrega).ValueGeneratedOnAdd();

        builder.Property(x => x.Metodo)
            .HasColumnName("metodo")
            .HasColumnType("char(1)")
            .IsRequired()
            .HasConversion(
                v => v == TipoEntrega.Presencial ? "P" : "F",

                v => v == "P" ? TipoEntrega.Presencial : TipoEntrega.Frete
            );
    }
}