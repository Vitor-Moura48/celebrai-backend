using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class CartaoMap : BaseMap<Cartao>
{
    public CartaoMap() : base("Cartao") { }

    public override void Configure(EntityTypeBuilder<Cartao> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdCartao);
        builder.Property(x => x.IdCartao).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Pagamento)
               .WithMany()
               .HasForeignKey(x => x.IdPagamento)
               .HasConstraintName("FK_Cartao_Pagamento");
    }
}
