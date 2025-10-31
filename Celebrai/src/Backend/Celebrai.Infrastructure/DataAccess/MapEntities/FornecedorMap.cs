using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class FornecedorMap : BaseMap<Fornecedor>
{
    public FornecedorMap() : base("Fornecedor")
    {
    }

    public override void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdFornecedor);
        builder.Property(x => x.IdFornecedor).HasColumnName("IdFornecedor");

        builder.Property(x => x.IdUsuario).HasColumnName("IdUsuario").IsRequired();

        builder.HasOne(x => x.Usuario) 
               .WithMany() 
               .HasForeignKey(x => x.IdUsuario)
               .HasConstraintName("FK_Fornecedor_Usuario"); 
    }
}
