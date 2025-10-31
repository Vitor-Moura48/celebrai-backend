using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PedidoProdutoMap : BaseMap<PedidoProduto>
{
    public PedidoProdutoMap() : base("PedidoProduto") { }

    public override void Configure(EntityTypeBuilder<PedidoProduto> builder)
    {
        base.Configure(builder);

        // Chave Primária Composta (PK)
        builder.HasKey(x => new { x.IdPedido, x.IdProduto });

        // Chave Estrangeira (FK) para Pedido
        builder.HasOne(x => x.Pedido)
               .WithMany()
               .HasForeignKey(x => x.IdPedido)
               .HasConstraintName("FK_PedidoProduto_Pedido");

        // Chave Estrangeira (FK) para Produto
        builder.HasOne(x => x.Produto)
               .WithMany()
               .HasForeignKey(x => x.IdProduto)
               .HasConstraintName("FK_PedidoProduto_Produto");
    }
}
