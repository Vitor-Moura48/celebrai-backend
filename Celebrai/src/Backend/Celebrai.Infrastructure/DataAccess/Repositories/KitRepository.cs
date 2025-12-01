using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Kit;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;

public class KitRepository : IKitWriteOnlyRepository, IKitReadOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public KitRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Kit kit, IEnumerable<ProdutoKit> produtosDoKit)
    {
        await _context.Kit.AddAsync(kit);
        await _context.ProdutoKit.AddRangeAsync(produtosDoKit);
    }

    public async Task<Kit?> GetByIdAsync(int id)
    {
        return await _context.Kit
            .AsNoTracking()
            .Include(kit => kit.ProdutosKit)
                .ThenInclude(produtoKit => produtoKit.Produto)
                    .ThenInclude(produto => produto.SubCategoria)
            .FirstOrDefaultAsync(kit => kit.IdKit == id);
    }

    public async Task<IList<Kit>> GetKitsList(int? page)
    {
        const int pageSize = 15;

        var query = _context.Kit.AsQueryable();

        query = query.OrderBy(kit => kit.IdKit);

        if (page is not null && page > 0)
            query = query.Skip(((int)page - 1) * pageSize);

        return await query
            .AsNoTracking()
            .Take(pageSize)
            .Include(kit => kit.ProdutosKit)
            .ThenInclude(produtosKit => produtosKit.Produto)
            .ThenInclude(produto => produto.SubCategoria)
            .ToListAsync();
    }

    public async Task<bool> ExistKitWithIdentifier(int idKit)
        => await _context.Kit.AnyAsync(kit => kit.IdKit.Equals(idKit));
}

