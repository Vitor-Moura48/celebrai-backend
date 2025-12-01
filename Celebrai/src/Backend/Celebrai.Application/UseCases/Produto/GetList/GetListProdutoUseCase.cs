using Celebrai.Communication.Responses.Produto;
using Celebrai.Domain.Repositories.Produto;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Produto.GetList;

public class GetListProdutoUseCase : IGetListProdutoUseCase
{
    private readonly IProdutoReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetListProdutoUseCase(
        IProdutoReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IList<ResponseProdutoJson>> Execute(int? page)
    {
        var products = await _repository.GetProdutosList(page);

        var response = _mapper.Map<IList<ResponseProdutoJson>>(products);

        return response;
    }
}
