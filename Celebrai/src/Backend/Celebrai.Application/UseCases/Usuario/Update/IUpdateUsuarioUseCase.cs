using Celebrai.Communication.Requests.Usuario;

namespace Celebrai.Application.UseCases.Usuario.Update;
public interface IUpdateUsuarioUseCase
{
    public Task Execute(RequestUpdateUsuarioJson request);
}
