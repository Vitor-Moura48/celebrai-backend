using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Usuario;
using Celebrai.Domain.Entities;
using Mapster;

namespace Celebrai.Application.Services.Mapster;
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        RequestToDomain(config);
        DomainToResponse(config);
    }

    private void RequestToDomain(TypeAdapterConfig config)
    {
        config.NewConfig<RequestRegisterUsuarioJson, Usuario>();

        config.NewConfig<RequestRegisterProdutoFormData, Produto>();
    }

    private void DomainToResponse(TypeAdapterConfig config)
    {
        config.NewConfig<ResponseRegisteredUsuarioJson, Usuario>();
        config.NewConfig<ResponseUsuarioProfileJson, Usuario>();
    }
}
