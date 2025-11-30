using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.FornecedorPedido;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class FornecedorPedidoRepository : IFornecedorPedidoReadOnlyRepository, IFornecedorPedidoWriteOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public FornecedorPedidoRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(FornecedorPedido fornecedorPedido)
    {
        await _context.FornecedorPedido.AddAsync(fornecedorPedido);
    }

    public async Task<List<FornecedorPedido>> GetListByFornecedorId(Guid fornecedorId)
        => await _context.FornecedorPedido.AsNoTracking().Where(fornecedorPedido => fornecedorPedido.IdFornecedor == fornecedorId).ToListAsync();
    public async Task<List<FornecedorPedido>> GetListByPedidoId(int pedidoId)
        => await _context.FornecedorPedido.AsNoTracking().Where(fornecedorPedido => fornecedorPedido.IdPedido == pedidoId).ToListAsync();
}