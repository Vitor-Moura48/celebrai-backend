using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.Pedido;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class PedidoRepository : IPedidoReadOnlyRepository, IPedidoWriteOnlyRepository, IPedidoUpdateOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public PedidoRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(Pedido pedido)
    {
        await _context.Pedido.AddAsync(pedido);
    }

    public async Task<Pedido?> GetById(int pedidoIdentifier) 
        => await _context.Pedido.FirstOrDefaultAsync(pedido => pedido.IdPedido == pedidoIdentifier);

    public async Task<Pedido?> GetByUserId(Guid userId)
        => await _context.Pedido.AsNoTracking().FirstOrDefaultAsync(pedido => pedido.IdUsuario == userId);
   
    public void Update(Pedido pedido) => _context.Pedido.Update(pedido);
}