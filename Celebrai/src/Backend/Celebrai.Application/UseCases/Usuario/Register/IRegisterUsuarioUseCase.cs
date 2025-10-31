using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;

namespace Celebrai.Application.UseCases.Usuario.Register;
public interface IRegisterUsuarioUseCase
{
    public Task<ResponseRegisteredUsuarioJson> Execute(RequestRegisterUsuarioJson user);
}
