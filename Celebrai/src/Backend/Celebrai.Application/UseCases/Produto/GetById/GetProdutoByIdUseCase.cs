using Celebrai.Communication.Responses.Produto;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Produto.GetById;

public class GetProdutoByIdUseCase : IGetProdutoByIdUseCase
{
    private readonly IProdutoReadOnlyRepository _repository;
    private readonly IFornecedorReadOnlyRepository _supplierRepository;
    private readonly IUsuarioReadOnlyRepository _userRepository;
    private readonly IMapper _mapper;
    public GetProdutoByIdUseCase(
        IProdutoReadOnlyRepository repository,
        IFornecedorReadOnlyRepository supplierReadOnlyRepository,
        IUsuarioReadOnlyRepository userRepository,
        IMapper mapper)
    {
        _repository = repository;
        _supplierRepository = supplierReadOnlyRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseLongProdutoJson> Execute(int id)
    {
        var product = await _repository.GetProdutoByIdentifier(id);

        if (product is null)
            throw new NotFoundException("Produto não encontrado");

        var productSupplier = await _supplierRepository.GetByProductId(product.IdProduto);

        var response = _mapper.Map<ResponseLongProdutoJson>(product);
        
        if (productSupplier is not null)
        {
            var supplier = await _supplierRepository.GetByIdFornecedor(productSupplier.IdFornecedor);

            var user = await _userRepository.GetByIdUsuario(supplier!.IdUsuario);

            response.NomeFornecedor = user!.Nome;
        }

        if (product.ImagemUrl is not null)
            response.ImagemUrl = product.ImagemUrl;

        return response;
    }
}
