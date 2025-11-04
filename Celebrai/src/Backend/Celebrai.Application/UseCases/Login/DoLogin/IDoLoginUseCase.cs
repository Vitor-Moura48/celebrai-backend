using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;

namespace Celebrai.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    public Task<ResponseLoginUsuarioJson> Execute(RequestLoginJson request);
}
