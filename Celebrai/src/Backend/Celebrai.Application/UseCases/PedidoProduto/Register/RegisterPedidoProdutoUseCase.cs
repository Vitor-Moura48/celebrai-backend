using Celebrai.Communication.Requests.PedidoProduto;
using Celebrai.Communication.Responses.PedidoProduto;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.PedidoProduto;
using Celebrai.Domain.Repositories.Produto;
using Celebrai.Domain.Repositories.Pedido;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.PedidoProduto.Register;
public class RegisterPedidoProdutoUseCase : IRegisterPedidoProdutoUseCase
{
    private readonly IProdutoReadOnlyRepository _produtoReadOnlyRepository;
    private readonly IPedidoProdutoWriteOnlyRepository _pedidoProdutoWriteOnlyRepository;
    private readonly IPedidoReadOnlyRepository _pedidoReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterPedidoProdutoUseCase(
        IProdutoReadOnlyRepository produtoReadOnlyRepository,
        IPedidoProdutoWriteOnlyRepository pedidoProdutoWriteOnlyRepository,
        IPedidoReadOnlyRepository pedidoReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _produtoReadOnlyRepository = produtoReadOnlyRepository;
        _pedidoProdutoWriteOnlyRepository = pedidoProdutoWriteOnlyRepository;
        _pedidoReadOnlyRepository = pedidoReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredPedidoProdutoJson> Execute(RequestRegisterPedidoProdutoJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.PedidoProduto>(request);

        await _pedidoProdutoWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredPedidoProdutoJson
        {
            Message = "Relação Pedido Produto criada com sucesso."
        };
    }

    private async Task Validate(RequestRegisterPedidoProdutoJson request)
    {
        var validator = new RegisterPedidoProdutoValidator();
        var result = await validator.ValidateAsync(request);

        var produtoExist = await _produtoReadOnlyRepository.ExistProdutoWithIdentifier(request.IdProduto);

        if (!produtoExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O produto não foi encontrado na plataforma"));
        
        var pedidoExist = await _pedidoReadOnlyRepository.ExistPedidoWithIdentifier(request.IdPedido);

        if (!pedidoExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O Pedido não foi encontrado na plataforma"));
    

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}