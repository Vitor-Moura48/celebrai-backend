using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;

namespace Celebrai.Application.UseCases.Usuario.UpdateEmail;
public interface IUpdateEmailUsuarioUseCase
{
    public Task<ResponseRegisteredUsuarioJson> Execute(RequestEmailUsuarioJson request);
}
