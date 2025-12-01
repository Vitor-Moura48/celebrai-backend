using Celebrai.Communication.Requests.Usuario;

namespace Celebrai.Application.UseCases.Usuario.ChangeAddress;
public interface IChangeAddressUsuarioUseCase
{
    public Task Execute(RequestChangeAddressUsuarioJson request);
}
