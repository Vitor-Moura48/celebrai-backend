using Celebrai.Communication.Requests.Kit;
using Celebrai.Communication.Requests.Produto;
using Celebrai.Communication.Requests.Usuario;
using Celebrai.Communication.Responses.Disponibilidade;
using Celebrai.Communication.Responses.Kit;
using Celebrai.Communication.Responses.Produto;
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
        
        config.NewConfig<RequestKitJson, Kit>();
    }

    private void DomainToResponse(TypeAdapterConfig config)
    {
        config.NewConfig<Usuario, ResponseRegisteredUsuarioJson>();
        config.NewConfig<Usuario, ResponseUsuarioProfileJson>();

        config.NewConfig<Produto, ResponseProdutoJson>()
            .Map(dest => dest.SubCategoria, src => src.SubCategoria.Nome ?? string.Empty);
        config.NewConfig<Produto, ResponseShortProdutoJson>()
            .Map(dest => dest.SubCategoria, src => src.SubCategoria.Nome ?? string.Empty);
        config.NewConfig<Produto, ResponseLongProdutoJson>()
            .Map(dest => dest.SubCategoria, src => src.SubCategoria.Nome ?? string.Empty);

        config.NewConfig<Disponibilidade, ResponseDisponibilidadeJson>();
        
        config.NewConfig<Kit, ResponseRegisteredKitJson>();
        config.NewConfig<Kit, ResponseKitJson>()
            .Map(dest => dest.Produtos, src => src.ProdutosKit.Select(produtoKit => produtoKit.Produto));
    }
}
