using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public interface IRegisterFornecedorUseCase
{
    public Task<ResponseRegisteredFornecedorJson> Execute(RequestRegisterFornecedorJson fornecedor);
}
