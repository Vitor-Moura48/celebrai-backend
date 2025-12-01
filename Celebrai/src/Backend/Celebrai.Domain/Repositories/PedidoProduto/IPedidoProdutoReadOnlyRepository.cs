namespace Celebrai.Domain.Repositories.PedidoProduto;
public interface IPedidoProdutoReadOnlyRepository
{
    public Task<List<Entities.PedidoProduto>> GetListByProdutoId(int produtoId);
    public Task<List<Entities.PedidoProduto>> GetListByPedidoId(int pedidoId);
}