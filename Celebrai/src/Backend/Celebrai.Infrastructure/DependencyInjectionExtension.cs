using Celebrai.Domain.Repositories;
using Celebrai.Domain.Repositories.Usuario;
using Celebrai.Domain.Services.AuthService;
using Celebrai.Domain.Services.EmailService;
using Celebrai.Infrastructure.DataAccess;
using Celebrai.Infrastructure.DataAccess.Repositories;
using Celebrai.Infrastructure.Services.AuthService;
using Celebrai.Infrastructure.Services.EmailService;
using FirebaseAdmin;
using FluentMigrator.Runner;
using Google.Apis.Auth.OAuth2;
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
        AddFirebaseAuthService(services, configuration);
        AddSendGridService(services, configuration);
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

    private static void AddFirebaseAuthService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, FirebaseAuthService>();

        string credentialPath = configuration["Settings:Firebase:CredentialPath"]!;
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(credentialPath),
        });
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
}
