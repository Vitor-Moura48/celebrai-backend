using Celebrai.Communication.Requests.FornecedorPedido;
using Celebrai.Communication.Responses.FornecedorPedido;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.FornecedorPedido;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Repositories.Pedido;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.FornecedorPedido.Register;
public class RegisterFornecedorPedidoUseCase : IRegisterFornecedorPedidoUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorPedidoWriteOnlyRepository _fornecedorPedidoWriteOnlyRepository;
    private readonly IPedidoReadOnlyRepository _pedidoReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterFornecedorPedidoUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorPedidoWriteOnlyRepository fornecedorPedidoWriteOnlyRepository,
        IPedidoReadOnlyRepository pedidoReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorPedidoWriteOnlyRepository = fornecedorPedidoWriteOnlyRepository;
        _pedidoReadOnlyRepository = pedidoReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredFornecedorPedidoJson> Execute(RequestRegisterFornecedorPedidoJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.FornecedorPedido>(request);

        await _fornecedorPedidoWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredFornecedorPedidoJson
        {
            Message = "Relação Fornecedor-Pedido criada com sucesso."
        };
    }

    private async Task Validate(RequestRegisterFornecedorPedidoJson request)
    {
        var validator = new RegisterFornecedorPedidoValidator();
        var result = await validator.ValidateAsync(request);

        var fornecedorExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithFornecedorIdentifier(request.IdFornecedor);

        if (!fornecedorExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O fornecedor não foi encontrado na plataforma"));
        
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