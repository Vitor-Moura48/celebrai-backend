using Celebrai.Communication.Responses.Usuario;

namespace Celebrai.Application.UseCases.Usuario.ConfirmEmail;
public interface IConfirmEmailUsuarioUseCase
{
    public Task<ResponseConfirmEmailUsuariojson> Execute(string token);
}
