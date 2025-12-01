using Celebrai.Communication.Requests.PedidoKit;
using Celebrai.Communication.Responses.PedidoKit;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.PedidoKit;
using Celebrai.Domain.Repositories.Kit;
using Celebrai.Domain.Repositories.Pedido;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.PedidoKit.Register;
public class RegisterPedidoKitUseCase : IRegisterPedidoKitUseCase
{
    private readonly IKitReadOnlyRepository _kitReadOnlyRepository;
    private readonly IPedidoKitWriteOnlyRepository _pedidoKitWriteOnlyRepository;
    private readonly IPedidoReadOnlyRepository _pedidoReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterPedidoKitUseCase(
        IKitReadOnlyRepository kitReadOnlyRepository,
        IPedidoKitWriteOnlyRepository pedidoKitWriteOnlyRepository,
        IPedidoReadOnlyRepository pedidoReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _kitReadOnlyRepository = kitReadOnlyRepository;
        _pedidoKitWriteOnlyRepository = pedidoKitWriteOnlyRepository;
        _pedidoReadOnlyRepository = pedidoReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredPedidoKitJson> Execute(RequestRegisterPedidoKitJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.PedidoKit>(request);

        await _pedidoKitWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredPedidoKitJson
        {
            Message = "Relação Pedido Kit criada com sucesso."
        };
    }

    private async Task Validate(RequestRegisterPedidoKitJson request)
    {
        var validator = new RegisterPedidoKitValidator();
        var result = await validator.ValidateAsync(request);

        var kitExist = await _kitReadOnlyRepository.ExistKitWithIdentifier(request.IdKit);

        if (!kitExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O kit não foi encontrado na plataforma"));
        
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