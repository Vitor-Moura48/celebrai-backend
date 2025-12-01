using Celebrai.Communication.Responses.Produto;

namespace Celebrai.Application.UseCases.Produto.GetList;

public interface IGetListProdutoUseCase
{
    public Task<IList<ResponseProdutoJson>> Execute(int? page);
}
