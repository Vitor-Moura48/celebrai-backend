namespace Celebrai.Domain.Repositories.Pedido;
public interface IPedidoReadOnlyRepository
{
    public Task<Entities.Pedido?> GetById(int pedidoIdentifier);
    public Task<Entities.Pedido?> GetByUserId(Guid userId);
}