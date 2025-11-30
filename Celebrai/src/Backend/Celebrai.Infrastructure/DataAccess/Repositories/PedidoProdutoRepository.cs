using Celebrai.Domain.Entities;
using Celebrai.Domain.Repositories.PedidoProduto;
using Microsoft.EntityFrameworkCore;

namespace Celebrai.Infrastructure.DataAccess.Repositories;
public class PedidoProdutoRepository : IPedidoProdutoReadOnlyRepository, IPedidoProdutoWriteOnlyRepository
{
    private readonly CelebraiDbContext _context;

    public PedidoProdutoRepository(CelebraiDbContext context) => _context = context;

    public async Task Add(PedidoProduto pedidoProduto)
    {
        await _context.PedidoProduto.AddAsync(pedidoProduto);
    }

    public async Task<List<PedidoProduto>> GetListByProdutoId(int produtoId)
        => await _context.PedidoProduto.AsNoTracking().Where(pedidoProduto => pedidoProduto.IdProduto == produtoId).ToListAsync();
    public async Task<List<PedidoProduto>> GetListByPedidoId(int pedidoId)
        => await _context.PedidoProduto.AsNoTracking().Where(pedidoProduto => pedidoProduto.IdPedido == pedidoId).ToListAsync();
}