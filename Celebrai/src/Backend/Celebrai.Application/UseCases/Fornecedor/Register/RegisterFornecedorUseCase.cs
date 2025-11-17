using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Communication.Responses.Tokens;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public class RegisterFornecedorUseCase : IRegisterFornecedorUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorWriteOnlyRepository _fornecedorWriteOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterFornecedorUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorWriteOnlyRepository fornecedorWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorWriteOnlyRepository = fornecedorWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
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

        var token = _accessTokenGenerator.Generate(entity.IdUsuario, UserTokenType.AccessToken, entity.Usuario.Role.ToString());

        return new ResponseRegisteredFornecedorJson
        {
            Nome = entity.Usuario.Nome,
            Tokens = new ResponseTokensJson
            {
                AccessToken = token
            },
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