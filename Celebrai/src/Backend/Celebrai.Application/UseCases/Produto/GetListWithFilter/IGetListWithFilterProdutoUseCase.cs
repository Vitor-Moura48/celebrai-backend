using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses.Produto;

namespace Celebrai.Application.UseCases.Produto.GetListWithFilter;

public interface IGetListWithFilterProdutoUseCase
{
    public Task<IList<ResponseShortProdutoJson>> Execute(int? page, RequestFilterProdutoJson request);
}
