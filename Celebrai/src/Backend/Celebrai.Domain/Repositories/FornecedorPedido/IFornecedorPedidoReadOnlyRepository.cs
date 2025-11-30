namespace Celebrai.Domain.Repositories.FornecedorPedido;
public interface IFornecedorPedidoReadOnlyRepository
{
    public Task<List<Entities.FornecedorPedido>> GetListByFornecedorId(Guid fornecedorId);
    public Task<List<Entities.FornecedorPedido>> GetListByPedidoId(int pedidoId);
}