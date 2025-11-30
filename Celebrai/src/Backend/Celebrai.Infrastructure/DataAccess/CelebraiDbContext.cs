using Celebrai.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess;
public class CelebraiDbContext : DbContext
{
    public CelebraiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Fornecedor> Fornecedor{ get; set; }
    public DbSet<PessoaFisica> PessoaFisica { get; set; }
    public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<PedidoProduto> PedidoProduto { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<Kit> Kit { get; set; }
    public DbSet<ProdutoKit> ProdutoKit { get; set; }
    public DbSet<Servico> Servico { get; set; }
    public DbSet<FornecedorProduto> FornecedorProduto { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<SubCategoria> SubCategoria { get; set; }
    public DbSet<ModalidadeEntrega> ModalidadeEntrega { get; set; }
    public DbSet<ModalidadeEntregaFornecedor> ModalidadeEntregaFornecedor { get; set; }
    public DbSet<Pagamento> Pagamento { get; set; }
    public DbSet<Pix> Pix { get; set; }
    public DbSet<Cartao> Cartao { get; set; }
    public DbSet<Disponibilidade> Disponibilidade { get; set; } 
    public DbSet<FornecedorPedido> FornecedorPedido { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CelebraiDbContext).Assembly);
    }
}
