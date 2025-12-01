using Celebrai.Communication.Requests.Pedido;
using Celebrai.Communication.Responses.Pedido;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Pedido;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Pedido.Register;
public class RegisterPedidoUseCase : IRegisterPedidoUseCase
{
    private readonly IPedidoReadOnlyRepository _pedidoReadOnlyRepository;
    private readonly IPedidoWriteOnlyRepository _pedidoWriteOnlyRepository;
    private readonly IUsuarioReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterPedidoUseCase(
        IPedidoReadOnlyRepository pedidoReadOnlyRepository,
        IPedidoWriteOnlyRepository pedidoWriteOnlyRepository,
        IUsuarioReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _pedidoReadOnlyRepository = pedidoReadOnlyRepository;
        _pedidoWriteOnlyRepository = pedidoWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRegisteredPedidoJson> Execute(RequestRegisterPedidoJson request)
    {
        var loggedUser = await _loggedUser.User();
        
        await Validate(request);

        var user = await _userReadOnlyRepository.GetByIdUsuario(loggedUser.IdUsuario);

        var entity = _mapper.Map<Domain.Entities.Pedido>(request);

        entity.IdUsuario = user!.IdUsuario;

        await _pedidoWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();


        return new ResponseRegisteredPedidoJson
        {
            Message = "Pedido criado com sucesso."
        };
    }

    private async Task Validate(RequestRegisterPedidoJson request)
    {
        var validator = new RegisterPedidoValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}