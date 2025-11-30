using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;

public class FornecedorPedidoMap : BaseMap<FornecedorPedido>
{
    public FornecedorPedidoMap() : base("FornecedorPedidoMap")
    {
    }

    override public void Configure(EntityTypeBuilder<FornecedorPedido> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.IdFornecedor, x.IdPedido });

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_FornecedorPedido_Fornecedor");

        builder.HasOne(x => x.Pedido)
               .WithMany()
               .HasForeignKey(x => x.IdPedido)
               .HasConstraintName("FK_FornecedorPedido_Pedido");
    }
}
