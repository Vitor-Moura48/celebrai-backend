namespace Celebrai.Domain.Repositories.PedidoKit;
public interface IPedidoKitReadOnlyRepository
{
    public Task<List<Entities.PedidoKit>> GetListByKitId(int kitId);
    public Task<List<Entities.PedidoKit>> GetListByPedidoId(int pedidoId);
}