using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PedidoMap : BaseMap<Pedido>
{
    public PedidoMap() : base("Pedido") { }

    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdPedido);
        builder.Property(x => x.IdPedido).UseIdentityColumn(1000, 10);

        builder.HasOne(x => x.Usuario)
               .WithMany()
               .HasForeignKey(x => x.IdUsuario)
               .HasConstraintName("FK_Pedido_Usuario");

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_Pedido_Fornecedor");

        builder.HasOne(x => x.ModalidadeEntrega)
               .WithMany()
               .HasForeignKey(x => x.IdModalidadeEntrega)
               .HasConstraintName("FK_Pedido_ModalidadeEntrega");

        builder.HasOne(x => x.Pagamento)
               .WithMany()
               .HasForeignKey(x => x.IdPagamento)
               .HasConstraintName("FK_Pedido_Pagamento");
    }
}