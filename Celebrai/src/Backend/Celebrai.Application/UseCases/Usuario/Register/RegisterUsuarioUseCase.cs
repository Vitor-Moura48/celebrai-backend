using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;
using Microsoft.Extensions.Configuration;

namespace Celebrai.Application.UseCases.Usuario.Register;
public class RegisterUsuarioUseCase : IRegisterUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepository _userReadOnlyRepository;
    private readonly IUsuarioWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IMapper _mapper;

    public RegisterUsuarioUseCase(
        IUsuarioReadOnlyRepository userReadOnlyRepository, 
        IUsuarioWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        IEmailService emailService,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        IConfiguration configuration)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _emailService = emailService;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<ResponseRegisteredUsuarioJson> Execute(RequestRegisterUsuarioJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.Usuario>(request);
        entity.Role = RoleUsuario.Cliente;
        entity.Senha = _passwordEncripter.Encrypt(entity.Senha);

        await _userWriteOnlyRepository.Add(entity);

        await _unitOfWork.Commit();

        var verificationToken = _accessTokenGenerator.Generate(
            entity.IdUsuario,
            UserTokenType.AccountVerification,
            RoleUsuario.Cliente.ToString(),
            customExpirationMinutes: 15
        );

        var baseUrl = _configuration["AppUrl"];
        var confirmLink = $"{baseUrl}/usuario/confirm-email?token={Uri.EscapeDataString(verificationToken)}";

        var subject = "Confirme seu e-mail - Celebrai";
        var htmlContent = $@"
            <h2>Confirme seu e-mail</h2>
            <p>Olá {entity.Nome},</p>
            <p>Obrigado por se cadastrar no Celebrai!</p>
            <p>Clique no link abaixo para confirmar seu e-mail:</p>
            <a href='{confirmLink}'>Confirmar e-mail</a>
            <p>O link é válido por 15 minutos.</p>";

        await _emailService.SendEmail(entity.Email, subject, htmlContent);

        var token = _accessTokenGenerator.Generate(entity.IdUsuario, UserTokenType.AccessToken, RoleUsuario.Cliente.ToString());
        return new ResponseRegisteredUsuarioJson
        {
            Nome = entity.Nome,
            Message = "Conta criada com sucesso. Verifique seu e-mail para confirmar seu cadastro(inclusive sua caixa de SPAM)."
        };
    }

    private async Task Validate(RequestRegisterUsuarioJson request)
    {
        var validator = new RegisterUsuarioValidator();

        var result = await validator.ValidateAsync(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O e-mail já está registrado na plataforma"));

        var cpfExist = await _userReadOnlyRepository.ExistUserWithCpf(request.CpfUsuario);
        if (cpfExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O CPF já está registrado na plataforma"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
