using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Responses.Produto;
using Celebrai.Domain.Dtos;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Produto.GetListWithFilter;

public class GetListWithFilterProdutoUseCase : IGetListWithFilterProdutoUseCase
{
    private readonly IProdutoReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetListWithFilterProdutoUseCase(
        IProdutoReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IList<ResponseShortProdutoJson>> Execute(int? page, RequestFilterProdutoJson request)
    {
        Validate(request);

        var filter = new FilterProdutosDto
        {
            Nome = request.Nome,
            Categoria = request.Categoria
        };

        var produtos = await _repository.GetProdutosWithFilters(page, filter);

        var response = _mapper.Map<IList<ResponseShortProdutoJson>>(produtos);

        return response;
    }

    private static void Validate(RequestFilterProdutoJson request)
    {
        var validator = new GetListWithFilterProdutoValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).Distinct().ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
