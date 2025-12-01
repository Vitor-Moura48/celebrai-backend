using Celebrai.Communication.Requests.PedidoKit;
using Celebrai.Communication.Responses.PedidoKit;

namespace Celebrai.Application.UseCases.PedidoKit.Register;
public interface IRegisterPedidoKitUseCase
{
    public Task<ResponseRegisteredPedidoKitJson> Execute(RequestRegisterPedidoKitJson pedidoKit);
}
