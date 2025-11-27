using Celebrai.Communication.Responses.Produto;

namespace Celebrai.Application.UseCases.Produto.GetById;

public interface IGetProdutoByIdUseCase
{
    public Task<ResponseProdutoJson> Execute(int id);
}
