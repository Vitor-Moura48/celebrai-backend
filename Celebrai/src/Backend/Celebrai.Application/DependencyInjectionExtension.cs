using Celebrai.Application.Services.Mapster;
using Celebrai.Application.UseCases.Usuario.Register;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Celebrai.Application;
public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddMapster(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUsuarioUseCase, RegisterUsuarioUseCase>();
    }

    private static void AddMapster(IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(MapsterConfig).Assembly); 

        services.AddMapster();
    }
}
