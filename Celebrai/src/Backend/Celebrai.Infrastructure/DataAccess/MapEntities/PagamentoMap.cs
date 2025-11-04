using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PagamentoMap : BaseMap<Pagamento>
{
    public PagamentoMap() : base("Pagamento") { }

    public override void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdPagamento);
        builder.Property(x => x.IdPagamento).ValueGeneratedOnAdd();
    }
}
