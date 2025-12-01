using Celebrai.Communication.Responses.Fornecedor;

namespace Celebrai.Application.UseCases.Fornecedor.Delete;
public interface IDeleteFornecedorUseCase
{
    public Task<ResponseDeletedFornecedorJson> Execute();
}
