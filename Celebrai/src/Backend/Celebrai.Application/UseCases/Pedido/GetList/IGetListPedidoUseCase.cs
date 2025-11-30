using Celebrai.Communication.Responses.Pedido;

namespace Celebrai.Application.UseCases.Pedido.GetList;
public interface IGetListPedidoUseCase
{
    public Task<ResponseListPedidoJson> Execute();
}
