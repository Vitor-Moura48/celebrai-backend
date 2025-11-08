using Celebrai.Communication.Requests.Usuario;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Celebrai.Application.UseCases.Usuario.Update;
public class UpdateUsuarioUseCase : IUpdateUsuarioUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuarioUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUsuarioUseCase(
        ILoggedUser loggedUser,
        IUsuarioUpdateOnlyRepository userUpdateOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUsuarioJson request)
    {
        var loggedUser = await _loggedUser.User();

        await Validate(request);

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.IdUsuario);

        user!.Nome = request.Nome;
        user.Celular = request.Telefone;

        _userUpdateOnlyRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUsuarioJson request)
    {
        var validator = new UpdateUsuarioValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorValidationException(errorMessages);
        }
    }
}
