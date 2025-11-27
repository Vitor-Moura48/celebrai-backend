using Celebrai.Communication.Responses.Produto;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Produto.GetById;

public class GetProdutoByIdUseCase : IGetProdutoByIdUseCase
{
    private readonly IProdutoReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetProdutoByIdUseCase(
        IProdutoReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseProdutoJson> Execute(int id)
    {
        var product = await _repository.GetProdutoByIdentifier(id);

        if (product is null)
            throw new NotFoundException("Produto não encontrado");

        var response = _mapper.Map<ResponseProdutoJson>(product);

        if (product.ImagemUrl is not null)
            response.ImagemUrl = product.ImagemUrl;

        return response;
    }
}
