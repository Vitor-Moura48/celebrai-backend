using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Communication.Responses.Tokens;
using Celebrai.Domain.Enums;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Fornecedor.Register;
public class RegisterFornecedorUseCase : IRegisterFornecedorUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorWriteOnlyRepository _fornecedorWriteOnlyRepository;
    private readonly IUsuarioUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterFornecedorUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorWriteOnlyRepository fornecedorWriteOnlyRepository,
        IUsuarioUpdateOnlyRepository userUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorWriteOnlyRepository = fornecedorWriteOnlyRepository;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseRegisteredFornecedorJson> Execute(RequestRegisterFornecedorJson request)
    {
        var loggedUser = await _loggedUser.User();
        await Validate(request, loggedUser);

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.IdUsuario);

        var entity = _mapper.Map<Domain.Entities.Fornecedor>(request);

        entity.IdUsuario = user!.IdUsuario;
        user.Role = RoleUsuario.Fornecedor;

        entity.Usuario = user;
        await _fornecedorWriteOnlyRepository.Add(entity);

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

    private async Task Validate(RequestRegisterFornecedorJson request, Domain.Entities.Usuario loggedUser)
    {
        var validator = new RegisterFornecedorValidator();

        var result = await validator.ValidateAsync(request);

        var userExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithIdentifier(loggedUser.IdUsuario);

        if (userExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O Usuário já está com conta de fornecedor na plataforma"));

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