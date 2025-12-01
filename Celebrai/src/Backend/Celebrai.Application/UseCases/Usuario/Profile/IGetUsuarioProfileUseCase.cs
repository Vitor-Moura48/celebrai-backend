using Celebrai.Communication.Requests.Usuario;

namespace Celebrai.Application.UseCases.Usuario.Profile;
public interface IGetUsuarioProfileUseCase
{
    public Task<ResponseUsuarioProfileJson> Execute();
}
