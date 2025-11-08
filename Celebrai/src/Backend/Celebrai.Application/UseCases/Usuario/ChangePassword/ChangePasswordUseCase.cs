using Celebrai.Communication.Requests.Usuario;
using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Exceptions.ExceptionsBase;

namespace Celebrai.Application.UseCases.Usuario.ChangePassword;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUsuarioUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(
        ILoggedUser loggedUser,
        IPasswordEncripter passwordEncripter,
        IUsuarioUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }
    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.IdUsuario);

        user!.Senha = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.Usuario loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);

        if (_passwordEncripter.IsValid(request.Password, loggedUser.Senha) == false)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "A senha inserida é diferente da senha atual"));

        if (result.IsValid == false)
            throw new ErrorValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
