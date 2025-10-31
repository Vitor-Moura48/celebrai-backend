using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Tokens;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.AuthService;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Usuario.Register;
public class RegisterUsuarioUseCase : IRegisterUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepository _userReadOnlyRepository;
    private readonly IUsuarioWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public RegisterUsuarioUseCase(
        IUsuarioReadOnlyRepository userReadOnlyRepository, 
        IUsuarioWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAuthService authService,
        IMapper mapper,
        IEmailService emailService)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _authService = authService;
        _mapper = mapper;
        _emailService = emailService;
    }
    public async Task<ResponseRegisteredUsuarioJson> Execute(RequestRegisterUsuarioJson request)
    {
        await Validate(request);

        string firebaseUid = await _authService.CreateAuthAccount(request.Email, request.Senha);

        string verificationLink = await _authService.GenerateEmailVerificationLink(request.Email);

        string emailBody = $"Bem-vindo, {request.Nome}!<br><br>" +
            $"Por favor, verifique seu e-mail clicando no link abaixo (verifique também sua pasta de SPAM):<br>" +
            $"<a href='{verificationLink}'>Verificar E-mail</a><br><br>" +
            $"Após a verificação, você poderá acessar normalmente o sistema.";

        string subject = "Verifique seu e-mail";

        await _emailService.SendEmail(request.Email, subject, emailBody);

        var entity = _mapper.Map<Domain.Entities.Usuario>(request);

        entity.IdExterno = firebaseUid;
        entity.Role = RoleUsuario.Cliente;

        await _userWriteOnlyRepository.Add(entity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUsuarioJson
        {
            Nome = entity.Nome,
            Tokens = new ResponseTokenFirebaseJson
            {
                AccessToken = await _authService.CreateCustomToken(entity.IdExterno, entity.Role.ToString())
            }
        };
    }

    private async Task Validate(RequestRegisterUsuarioJson request)
    {
        var validator = new RegisterUsuarioValidator();

        var result = await validator.ValidateAsync(request);

        var emailExist = await _userReadOnlyRepository.ExistUserWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O e-mail já está registrado na plataforma"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
