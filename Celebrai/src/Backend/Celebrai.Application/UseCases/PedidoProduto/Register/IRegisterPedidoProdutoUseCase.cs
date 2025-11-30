using Celebrai.Communication.Requests.PedidoProduto;
using Celebrai.Communication.Responses.PedidoProduto;

namespace Celebrai.Application.UseCases.PedidoProduto.Register;
public interface IRegisterPedidoProdutoUseCase
{
    public Task<ResponseRegisteredPedidoProdutoJson> Execute(RequestRegisterPedidoProdutoJson pedidoProduto);
}
