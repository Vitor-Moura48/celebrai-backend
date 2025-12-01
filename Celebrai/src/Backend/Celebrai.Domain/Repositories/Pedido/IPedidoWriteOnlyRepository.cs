namespace Celebrai.Domain.Repositories.Pedido;
public interface IPedidoWriteOnlyRepository
{
    public Task Add(Entities.Pedido pedido);
}