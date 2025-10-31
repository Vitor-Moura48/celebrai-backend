using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class PessoaFisicaMap : BaseMap<PessoaFisica>
{
    public PessoaFisicaMap() : base("PessoaFisica")
    {
    }

    public override void Configure(EntityTypeBuilder<PessoaFisica> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Cpf);
        builder.Property(x => x.Cpf).HasColumnName("Cpf");

        builder.Property(x => x.IdFornecedor).HasColumnName("IdFornecedor").IsRequired();

        builder.HasOne(x => x.Fornecedor) 
               .WithMany() 
               .HasForeignKey(x => x.IdFornecedor)
               .HasConstraintName("FK_PessoaFisica_Fornecedor"); 
    }
}
