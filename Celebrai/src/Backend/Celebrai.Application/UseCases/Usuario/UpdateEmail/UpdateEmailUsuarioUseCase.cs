
using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using Microsoft.Extensions.Configuration;

namespace Celebrai.Application.UseCases.Usuario.UpdateEmail;
public class UpdateEmailUsuarioUseCase : IUpdateEmailUsuarioUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuarioUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUsuarioReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    public UpdateEmailUsuarioUseCase(
        ILoggedUser loggedUser,
        IUsuarioUpdateOnlyRepository userUpdateOnlyRepository,
        IUsuarioReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _loggedUser = loggedUser;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<ResponseRegisteredUsuarioJson> Execute(RequestEmailUsuarioJson request)
    {
        var loggedUser = await _loggedUser.User();

        var currentEmail = loggedUser.Email;

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
                throw new ErrorValidationException(["Email já registrado na plataforma"]);
        }

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.IdUsuario);

        user!.Email = request.Email;
        user.Ativo = false;

        _userUpdateOnlyRepository.Update(user);

        await _unitOfWork.Commit();

        var verificationToken = _accessTokenGenerator.Generate(
            user.IdUsuario,
            UserTokenType.AccountVerification,
            RoleUsuario.Cliente.ToString(),
            customExpirationMinutes: 15
        );

        var baseUrl = _configuration["AppUrl"];
        var confirmLink = $"{baseUrl}/usuario/confirm-email?token={Uri.EscapeDataString(verificationToken)}";

        var subject = "Confirme seu e-mail - Celebrai";
        var htmlContent = $@"
            <h2>Confirme seu e-mail</h2>
            <p>Olá {user.Nome},</p>
            <p>Obrigado por se cadastrar no Celebrai!</p>
            <p>Clique no link abaixo para confirmar seu e-mail:</p>
            <a href='{confirmLink}'>Confirmar e-mail</a>
            <p>O link é válido por 15 minutos.</p>";

        await _emailService.SendEmail(user.Email, subject, htmlContent);

        return new ResponseRegisteredUsuarioJson
        {
            Nome = user.Nome,
            Message = "o e-mail atualizado com sucesso. Verifique seu e-mail para confirmar seu e-mail(inclusive sua caixa de SPAM)."
        };
    }
}
