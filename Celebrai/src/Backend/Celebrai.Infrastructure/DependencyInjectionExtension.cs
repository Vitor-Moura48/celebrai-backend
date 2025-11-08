using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Security.Cryptography;
using Celebrai.Domain.Security.Tokens;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Domain.Services.LoggedUser;
using Celebrai.Infrastructure.DataAccess;
using Celebrai.Infrastructure.DataAccess.Repositories;
using Celebrai.Infrastructure.Security.Cryptography;
using Celebrai.Infrastructure.Security.Tokens.Access.Generator;
using Celebrai.Infrastructure.Security.Tokens.Access.Validator;
using Celebrai.Infrastructure.Services.EmailService;
using Celebrai.Infrastructure.Services.LoggedUser;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;
using System.Reflection;

namespace Celebrai.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddFluentMigrator(services, configuration);
        AddTokens(services, configuration);
        AddSendGridService(services, configuration);
        AddPasswordEncrpter(services);
        AddLoggedUser(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionString");

        services.AddDbContext<CelebraiDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioWriteOnlyRepository, UsuarioRepository>();   
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionString");

        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.Load("Celebrai.Infrastructure")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = uint.Parse(configuration["Settings:Jwt:ExpirationTimeMinutes"]!);
        var signingKey = configuration["Settings:Jwt:SigningKey"];

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddSendGridService(IServiceCollection services, IConfiguration configuration)
    {
        string fromEmail = configuration["Settings:SendGrid:FromEmail"]!;
        string fromName = configuration["Settings:SendGrid:FromName"]!;
        string sendGridApiKey = configuration["Settings:SendGrid:ApiKey"]!;

        services.AddSendGrid(options =>
        {
            options.ApiKey = sendGridApiKey;
        });

        services.AddScoped<IEmailService>(sp =>
        {
            var sendGridClient = sp.GetRequiredService<ISendGridClient>();
            return new SendGridEmailService(sendGridClient, fromEmail, fromName);
        });
    }

    private static void AddPasswordEncrpter(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncripter, BCryptNet>();
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
}
