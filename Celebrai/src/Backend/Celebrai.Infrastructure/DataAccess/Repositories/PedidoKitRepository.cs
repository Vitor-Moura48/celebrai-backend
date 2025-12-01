using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.PedidoKit;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class PedidoKitRepository : IPedidoKitReadOnlyRepository, IPedidoKitWriteOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public PedidoKitRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(PedidoKit pedidoKit)
    {
        await _context.PedidoKit.AddAsync(pedidoKit);
    }

    public async Task<List<PedidoKit>> GetListByKitId(int kitId)
        => await _context.PedidoKit.AsNoTracking().Where(pedidoKit => pedidoKit.IdKit == kitId).ToListAsync();
    public async Task<List<PedidoKit>> GetListByPedidoId(int pedidoId)
        => await _context.PedidoKit.AsNoTracking().Where(pedidoKit => pedidoKit.IdPedido == pedidoId).ToListAsync();
}