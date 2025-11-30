namespace Celebrai.Domain.Repositories.FornecedorPedido;
public interface IFornecedorPedidoWriteOnlyRepository
{
    public Task Add(Entities.FornecedorPedido fornecedorPedido);
}