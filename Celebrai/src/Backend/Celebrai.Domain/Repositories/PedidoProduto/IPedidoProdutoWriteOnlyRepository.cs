namespace Celebrai.Domain.Repositories.PedidoProduto;
public interface IPedidoProdutoWriteOnlyRepository
{
    public Task Add(Entities.PedidoProduto pedidoProduto);
}