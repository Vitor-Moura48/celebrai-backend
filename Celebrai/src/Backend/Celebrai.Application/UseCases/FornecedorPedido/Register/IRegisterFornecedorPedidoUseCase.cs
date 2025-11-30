using Celebrai.Communication.Requests.FornecedorPedido;
using Celebrai.Communication.Responses.FornecedorPedido;

namespace Celebrai.Application.UseCases.FornecedorPedido.Register;
public interface IRegisterFornecedorPedidoUseCase
{
    public Task<ResponseRegisteredFornecedorPedidoJson> Execute(RequestRegisterFornecedorPedidoJson fornecedorPedido);
}
