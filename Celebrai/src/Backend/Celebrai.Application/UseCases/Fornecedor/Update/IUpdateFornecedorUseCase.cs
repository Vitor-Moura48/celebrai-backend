using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;

namespace Celebrai.Application.UseCases.Fornecedor.Update;

public interface IUpdateFornecedorUseCase
{
    Task<ResponseUpdateFornecedorJson> Execute(RequestUpdateFornecedorJson request);
}
