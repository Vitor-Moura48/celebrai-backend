using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Produto;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class ProdutoRepository : IProdutoWriteOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public ProdutoRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Produto product, FornecedorProduto supplier)
    {
        await _context.Produto.AddAsync(product);
        await _context.FornecedorProduto.AddAsync(supplier);
    }
}
