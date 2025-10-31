using Celebrai.Domain.Repositories;

namespace Celebrai.Infrastructure.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    private readonly CelebraiDbContext _context;

    public UnitOfWork(CelebraiDbContext context) => _context = context;

    public async Task Commit() => await _context.SaveChangesAsync();
}
