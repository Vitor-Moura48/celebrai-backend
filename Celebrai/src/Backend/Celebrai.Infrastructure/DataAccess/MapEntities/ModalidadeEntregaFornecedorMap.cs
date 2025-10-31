using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class ModalidadeEntregaFornecedorMap : BaseMap<ModalidadeEntregaFornecedor>
{
    public ModalidadeEntregaFornecedorMap() : base("ModalidadeEntregaFornecedor") { }

    public override void Configure(EntityTypeBuilder<ModalidadeEntregaFornecedor> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.IdModalidadeEntrega, x.IdFornecedor });

        builder.HasOne(x => x.ModalidadeEntrega)
               .WithMany()
               .HasForeignKey(x => x.IdModalidadeEntrega)
               .HasConstraintName("FK_ModEntregaForn_ModEntrega");

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_ModEntregaForn_Fornecedor");
    }
}
