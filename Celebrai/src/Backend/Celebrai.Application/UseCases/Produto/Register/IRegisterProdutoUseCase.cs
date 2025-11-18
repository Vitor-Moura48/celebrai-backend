using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses.Produto;

namespace Celebrai.Application.UseCases.Produto.Register;
public interface IRegisterProdutoUseCase
{
    public Task<ResponseRegisteredProdutoJson> Execute(RequestRegisterProdutoFormData request);
}
