using Celebrai.Communication.Requests.Fornecedor;

namespace Celebrai.Application.UseCases.Fornecedor.Profile;
public interface IGetFornecedorProfileUseCase
{
    public Task<ResponseFornecedorProfileJson> Execute();
}
