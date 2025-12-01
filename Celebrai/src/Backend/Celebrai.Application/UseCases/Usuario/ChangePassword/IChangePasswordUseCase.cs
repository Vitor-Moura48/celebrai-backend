using Celebrai.Communication.Requests.Usuario;

namespace Celebrai.Application.UseCases.Usuario.ChangePassword;
public interface IChangePasswordUseCase
{
    public Task Execute(RequestChangePasswordJson request);
}
