using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Celebrai.Infrastructure.DataAccess.MapEntities;
public class UsuarioMap : BaseMap<Usuario>
{
    public UsuarioMap() : base("Usuario")
    {
    }

    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.IdUsuario);
        builder.Property(x => x.IdUsuario).HasColumnName("IdUsuario");

        builder.Property(u => u.Role).HasConversion<string>();
    }
}