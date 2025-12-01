using Celebrai.Communication.Responses.Fornecedor;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Fornecedor;
using Celebrai.Domain.Services.LoggedUser;

namespace Celebrai.Application.UseCases.Fornecedor.Delete;
public class DeleteFornecedorUseCase : IDeleteFornecedorUseCase
{
    private readonly IFornecedorReadOnlyRepository _fornecedorReadOnlyRepository;
    private readonly IFornecedorUpdateOnlyRepository _fornecedorUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteFornecedorUseCase(
        IFornecedorReadOnlyRepository fornecedorReadOnlyRepository,
        IFornecedorUpdateOnlyRepository fornecedorUpdateRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser
        )
    {
        _fornecedorReadOnlyRepository = fornecedorReadOnlyRepository;
        _fornecedorUpdateRepository = fornecedorUpdateRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseDeletedFornecedorJson> Execute()
    {

        var loggedUser = await _loggedUser.User();

        var fornecedor = await _fornecedorReadOnlyRepository.GetByUserId(loggedUser.IdUsuario);

        fornecedor!.Ativo = false;
        _fornecedorUpdateRepository.Update(fornecedor);
        await _unitOfWork.Commit(); 

        return new ResponseDeletedFornecedorJson
        {
            Message = "Conta Fornecedor Desativada com sucesso."
        };
    }
}