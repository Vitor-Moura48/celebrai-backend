using Celebrai.Communication.Requests.Pedido;
using Celebrai.Communication.Responses.Pedido;

namespace Celebrai.Application.UseCases.Pedido.Register;
public interface IRegisterPedidoUseCase
{
    public Task<ResponseRegisteredPedidoJson> Execute(RequestRegisterPedidoJson pedido);
}
