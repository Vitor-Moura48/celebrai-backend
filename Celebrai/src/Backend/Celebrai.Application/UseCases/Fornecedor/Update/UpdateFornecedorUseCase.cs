using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Communication.Requests.Fornecedor;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Fornecedor.Update;

public class UpdateFornecedorUseCase : IUpdateFornecedorUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorUpdateOnlyRepository _fornecedorUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateFornecedorUseCase(
        ILoggedUser loggedUser,
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorUpdateOnlyRepository fornecedorUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _loggedUser = loggedUser;
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorUpdateOnlyRepository = fornecedorUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseUpdateFornecedorJson> Execute(RequestUpdateFornecedorJson request)
    {
        var loggedUser = await _loggedUser.User();
        
        var fornecedor = await _fornecedorReadOnlyRepository.GetByUserId(loggedUser.IdUsuario);

        await Validate(request, loggedUser);
        
        _mapper.Map(request, fornecedor);
        
        _fornecedorUpdateOnlyRepository.Update(fornecedor!);
        
        await _unitOfWork.Commit();

        return new ResponseUpdateFornecedorJson
        {
            Message = "Conta Fornecedor Atualizada com sucesso."
        };
    }

    private async Task Validate(RequestUpdateFornecedorJson request, Domain.Entities.Usuario loggedUser)
    {
        var validator = new UpdateFornecedorValidator();
        var result = await validator.ValidateAsync(request);

        var userExist = await _fornecedorReadOnlyRepository.ExistActiveFornecedorWithIdentifier(loggedUser.IdUsuario);
        if (!userExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "O Fornecedor não foi encontrado"));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorValidationException(errorMessages);
        }
    }
}
