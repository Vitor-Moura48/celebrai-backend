using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;

using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.EmailService;

using Celebrai.Exceptions.ExceptionsBase;

using MapsterMapper;
using Microsoft.Extensions.Configuration;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public class RegisterFornecedorUseCase : IRegisterFornecedorUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorWriteOnlyRepository _fornecedorWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IMapper _mapper;

    public RegisterFornecedorUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository, 
        IFornecedorWriteOnlyRepository fornecedorWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        IEmailService emailService,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        IConfiguration configuration)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorWriteOnlyRepository = fornecedorWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _emailService = emailService;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
        _configuration = configuration;
    }
    public async Task<ResponseRegisteredFornecedorJson> Execute(RequestRegisterFornecedorJson request)
    {
        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.Fornecedor>(request);

        Console.WriteLine("---------------");
        Console.WriteLine(entity.Usuario == null ? entity : entity.Usuario.ToString());
        Console.WriteLine("---------------");

        entity.Usuario.Role = RoleUsuario.Fornecedor;
        entity.Usuario.Senha = _passwordEncripter.Encrypt(entity.Usuario.Senha);

        await _fornecedorWriteOnlyRepository.Add(entity);

        await _unitOfWork.Commit();

        var verificationToken = _accessTokenGenerator.Generate(
            entity.IdFornecedor,
            UserTokenType.AccountVerification,
            RoleUsuario.Fornecedor.ToString(),
            customExpirationMinutes: 15
        );

        var baseUrl = _configuration["AppUrl"];
        var confirmLink = $"{baseUrl}/fornecedor/confirm-email?token={Uri.EscapeDataString(verificationToken)}";

        var subject = "Confirme seu e-mail - Celebrai";
        var htmlContent = $@"
            <h2>Confirme seu e-mail</h2>
            <p>Olá {entity.Usuario.Nome},</p>
            <p>Obrigado por se cadastrar no Celebrai!</p>
            <p>Clique no link abaixo para confirmar seu e-mail:</p>
            <a href='{confirmLink}'>Confirmar e-mail</a>
            <p>O link é válido por 15 minutos.</p>";

        await _emailService.SendEmail(entity.Usuario.Email, subject, htmlContent);

        var token = _accessTokenGenerator.Generate(entity.IdFornecedor, UserTokenType.AccessToken, RoleUsuario.Fornecedor.ToString());
        return new ResponseRegisteredFornecedorJson
        {
            Nome = entity.Usuario.Nome,
            Message = "Conta criada com sucesso. Verifique seu e-mail para confirmar seu cadastro(inclusive sua caixa de SPAM)."
        };
    }

    private async Task Validate(RequestRegisterFornecedorJson request)
    {
        var validator = new RegisterFornecedorValidator();

        var result = await validator.ValidateAsync(request);

        var emailExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithEmail(request.Email);
        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O e-mail já está registrado na plataforma"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
