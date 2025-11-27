using Celebrai.Domain.Dtos;
using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Produto;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class ProdutoRepository : IProdutoWriteOnlyRepository, IProdutoReadOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public ProdutoRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Produto product, FornecedorProduto supplier)
    {
        await _context.Produto.AddAsync(product);
        await _context.FornecedorProduto.AddAsync(supplier);
    }

    public async Task<Produto?> GetProdutoByIdentifier(int id)
        => await _context.Produto
            .AsNoTracking()
            .Include(p => p.SubCategoria)
            .FirstOrDefaultAsync(product => product.IdProduto == id);

    public async Task<IList<Produto>> GetProdutosList(int? page)
    {
        const int pageSize = 15;

        var query = _context.Produto.AsQueryable();

        if (page is not null && page > 0)
            query = query.Skip(((int)page - 1) * pageSize);

        return await query
            .AsNoTracking()
            .Include(p => p.SubCategoria)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IList<Produto>> GetProdutosWithFilters(int? page, FilterProdutosDto filter)
    {
        const int pageSize = 15;

        var query = _context.Produto.AsQueryable();

        if (string.IsNullOrWhiteSpace(filter.Nome) is false)
            query = query.Where(product => product.Nome.Contains(filter.Nome));

        if (string.IsNullOrWhiteSpace(filter.Categoria) is false)
            query = query.Where(product => product.SubCategoria.Nome.Contains(filter.Categoria));

        if (page is not null && page > 0)
            query = query.Skip(((int)page - 1) * pageSize);

        return await query
            .AsNoTracking()
            .Include(p => p.SubCategoria)
            .Take(pageSize)
            .ToListAsync();
    }
}
