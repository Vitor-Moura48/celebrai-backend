using Celebrai.Domain.Entities;
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
    }
}