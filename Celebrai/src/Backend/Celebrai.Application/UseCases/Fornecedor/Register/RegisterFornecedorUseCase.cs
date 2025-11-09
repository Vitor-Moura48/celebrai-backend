using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;

using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Domain.Services.LoggedUser;

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

    private readonly ILoggedUser _loggedUser;

    public RegisterFornecedorUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorWriteOnlyRepository fornecedorWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        IEmailService emailService,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        IConfiguration configuration,
        ILoggedUser loggedUser)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorWriteOnlyRepository = fornecedorWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _emailService = emailService;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
        _configuration = configuration;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRegisteredFornecedorJson> Execute(RequestRegisterFornecedorJson request)
    {

        await Validate(request);

        var entity = _mapper.Map<Domain.Entities.Fornecedor>(request);

        var loggedUser = await _loggedUser.User();
        entity.IdUsuario = loggedUser.IdUsuario;
        entity.Usuario = loggedUser;
        entity.Usuario.Role = RoleUsuario.Fornecedor;

        await _fornecedorWriteOnlyRepository.Add(entity);
        await _unitOfWork.Commit();

        if (request.TipoFornecedor == "PF")
        {
            var pfEntity = _mapper.Map<Domain.Entities.PessoaFisica>(request);
            pfEntity.IdFornecedor = entity.IdFornecedor;
            pfEntity.NomeCompleto = request.NomeCompleto!;
            pfEntity.Cpf = request.CPF!;

            await _fornecedorWriteOnlyRepository.AddPessoaFisica(pfEntity);
        }
        else if (request.TipoFornecedor == "PJ")
        {
            var pjEntity = _mapper.Map<Domain.Entities.PessoaJuridica>(request);
            pjEntity.IdFornecedor = entity.IdFornecedor;
            pjEntity.RazaoSocial = request.RazaoSocial!;
            pjEntity.Cnpj = request.CNPJ!;

            await _fornecedorWriteOnlyRepository.AddPessoaJuridica(pjEntity);
        }

        await _unitOfWork.Commit();

        return new ResponseRegisteredFornecedorJson
        {
            Nome = entity.Usuario.Nome,
            Message = "Conta Fornecedor criada com sucesso."
        };
    }

    private async Task Validate(RequestRegisterFornecedorJson request)
    {
        var validator = new RegisterFornecedorValidator();

        var result = await validator.ValidateAsync(request);

        if (request.TipoFornecedor == "PF")
        {
            var cpfExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithCPF(request.CPF!);
            if (cpfExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O CPF já está registrado na plataforma"));
        }
        else if (request.TipoFornecedor == "PJ")
        {
            var cnpjExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithCNPJ(request.CNPJ!);
            if (cnpjExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O CNPJ já está registrado na plataforma"));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}