
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.LoggedUser;

namespace Celebrai.Application.UseCases.Usuario.Delete;
public class RequestDeleteUsuarioUseCase : IRequestDeleteUsuarioUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuarioUpdateOnlyRepository _userUpdateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestDeleteUsuarioUseCase(
        ILoggedUser loggedUser,
        IUsuarioUpdateOnlyRepository userUpdateRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userUpdateRepository = userUpdateRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Execute()
    {
        var loggedUser = await _loggedUser.User();

        var user = await _userUpdateRepository.GetById(loggedUser.IdUsuario);

        user!.Ativo = false;
        _userUpdateRepository.Update(user);

        await _unitOfWork.Commit();
    }
}
