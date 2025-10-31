using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class ServicoMap : BaseMap<Servico>
{
    public ServicoMap() : base("Servico") { }

    public override void Configure(EntityTypeBuilder<Servico> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdServico);
        builder.Property(x => x.IdServico).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Produto)
               .WithMany()
               .HasForeignKey(x => x.IdProduto)
               .HasConstraintName("FK_Servico_Produto");
    }
}
