namespace Celebrai.Domain.Repositories.PedidoKit;

public interface IPedidoKitWriteOnlyRepository
{
    public Task Add(Entities.PedidoKit pedidoKit);
}