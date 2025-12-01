using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PedidoKitMap : BaseMap<PedidoKit>
{
    public PedidoKitMap() : base("PedidoKit") { }

    public override void Configure(EntityTypeBuilder<PedidoKit> builder)
    {
        base.Configure(builder);

        // Chave Primária Composta (PK)
        builder.HasKey(x => new { x.IdPedido, x.IdKit });

        // Chave Estrangeira (FK) para Pedido
        builder.HasOne(x => x.Pedido)
               .WithMany()
               .HasForeignKey(x => x.IdPedido)
               .HasConstraintName("FK_PedidoKit_Pedido");

        // Chave Estrangeira (FK) para Kit
        builder.HasOne(x => x.Kit)
               .WithMany()
               .HasForeignKey(x => x.IdKit)
               .HasConstraintName("FK_PedidoKit_Kit");
    }
}
