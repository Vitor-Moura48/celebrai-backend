using Celebrai.Domain.Dtos;
using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Kit;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;

public class KitRepository : IKitWriteOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public KitRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Kit kit, IEnumerable<ProdutoKit> produtosDoKit)
    {
        await _context.Kit.AddAsync(kit);
        await _context.ProdutoKit.AddRangeAsync(produtosDoKit);
    }
}
