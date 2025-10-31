using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PessoaJuridicaMap : BaseMap<PessoaJuridica>
{
    public PessoaJuridicaMap() : base("PessoaJuridica")
    {

    }

    public override void Configure(EntityTypeBuilder<PessoaJuridica> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Cnpj);
        builder.Property(x => x.Cnpj).HasColumnName("Cnpj");

        builder.Property(x => x.IdFornecedor).HasColumnName("IdFornecedor").IsRequired();

        builder.HasOne(x => x.Fornecedor)
               .WithMany()
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_PessoaFisica_Fornecedor");
    }
}
