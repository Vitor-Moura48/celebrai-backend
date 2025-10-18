using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess;
public class CelebraiDbContext : DbContext
{
    public CelebraiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Fornecedor> Fornecedore{ get; set; }
    public DbSet<PessoaFisica> PessoaFisica { get; set; }
    public DbSet<PessoaJuridica> PessoaJuridica { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CelebraiDbContext).Assembly);
    }
}
