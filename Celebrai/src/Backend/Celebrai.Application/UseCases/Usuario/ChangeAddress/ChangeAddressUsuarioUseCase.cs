using Celebrai.Communication.Requests.Usuario;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;

namespace Celebrai.Application.UseCases.Usuario.ChangeAddress;
public class ChangeAddressUsuarioUseCase : IChangeAddressUsuarioUseCase
{
    private readonly IUsuarioUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    public ChangeAddressUsuarioUseCase(
        ILoggedUser loggedUser,
        IUsuarioUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute(RequestChangeAddressUsuarioJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request);

        var user = await _repository.GetById(loggedUser.IdUsuario);

        user!.Lograduro = request.Lograduro;
        user.Numero = request.Numero;
        user.CEP = request.CEP;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangeAddressUsuarioJson request)
    {
        var result = new ChangeAddressUsuarioValidator().Validate(request);

        if (result.IsValid == false)
        {
            throw new ErrorValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
