using Celebrai.Application.Services.Mapster;
using Celebrai.Application.UseCases.Fornecedor.Register;
using Celebrai.Application.UseCases.Fornecedor.Delete;
using Celebrai.Application.UseCases.Login.DoLogin;
using Celebrai.Application.UseCases.Produto.GetById;
using Celebrai.Application.UseCases.Produto.GetList;
using Celebrai.Application.UseCases.Produto.GetListWithFilter;
using Celebrai.Application.UseCases.Produto.Register;
using Celebrai.Application.UseCases.Usuario.ChangeAddress;
using Celebrai.Application.UseCases.Usuario.ChangePassword;
using Celebrai.Application.UseCases.Usuario.ConfirmEmail;
using Celebrai.Application.UseCases.Usuario.Delete;
using Celebrai.Application.UseCases.Usuario.Profile;
using Celebrai.Application.UseCases.Usuario.Register;
using Celebrai.Application.UseCases.Usuario.Update;
using Celebrai.Application.UseCases.Usuario.UpdateEmail;
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
        services.AddScoped<IConfirmEmailUsuarioUseCase, ConfirmEmailUsuarioUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IUpdateUsuarioUseCase, UpdateUsuarioUseCase>();
        services.AddScoped<IUpdateEmailUsuarioUseCase, UpdateEmailUsuarioUseCase>();
        services.AddScoped<IChangeAddressUsuarioUseCase, ChangeAddressUsuarioUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IGetUsuarioProfileUseCase, GetUsuarioProfileUseCase>();
        services.AddScoped<IRequestDeleteUsuarioUseCase, RequestDeleteUsuarioUseCase>();
        services.AddScoped<IRegisterFornecedorUseCase, RegisterFornecedorUseCase>();
        services.AddScoped<IDeleteFornecedorUseCase, DeleteFornecedorUseCase>();
        services.AddScoped<IRegisterProdutoUseCase, RegisterProdutoUseCase>();
        services.AddScoped<IGetProdutoByIdUseCase, GetProdutoByIdUseCase>();
        services.AddScoped<IGetListProdutoUseCase, GetListProdutoUseCase>();
        services.AddScoped<IGetListWithFilterProdutoUseCase, GetListWithFilterProdutoUseCase>();
    }

    private static void AddMapster(IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(typeof(MapsterConfig).Assembly); 

        services.AddMapster();
    }
}
