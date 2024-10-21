using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.BatePapo;
using DoseOfHope.Domain.Repositories.Permissions;
using DoseOfHope.Domain.Repositories.Produto;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Domain.Security.Tokens;
using DoseOfHope.Infrastructure.DataAcess;
using DoseOfHope.Infrastructure.DataAcess.Repositories;
using DoseOfHope.Infrastructure.Email.EnviaEmailAsync;
using DoseOfHope.Infrastructure.Email.Templates;
using DoseOfHope.Infrastructure.Encryption.Criptografar;
using DoseOfHope.Infrastructure.Encryption.Descriptografar;
using DoseOfHope.Infrastructure.Secutiry.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoseOfHope.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRespositories(services);
        AddSendEmail(services);
        AddEncryption(services, configuration);
        AddTokenGenerator(services, configuration);
    }
    public static void AddRespositories(IServiceCollection services)
    {
        services.AddScoped<IUsuariosReadOnlyRepository, UsuarioRepository>();
        services.AddScoped<IUsuariosWriteOnlyRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>();

        services.AddScoped<IProdutoReadOnlyRepository, ProdutoRepository>();    
        services.AddScoped<IProdutoWriteOnlyRepository, ProdutoRepository>();
        services.AddScoped<IProdutoUpdateOnlyRepository, ProdutoRepository>();

        services.AddScoped<IPermissionsReadOnlyRepository, PermissionRespository>();

        services.AddScoped<IBatePapoReadOnlyRepository, BatePapoRepository>();
        services.AddScoped<IBatePapoWriteOnlyRepository, BatePapoRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetConnectionString("Connection");
        services.AddDbContext<DoseOfHopeDbContext>(config => config.UseSqlServer(connectionStrings));
    }
    public static void AddSendEmail(IServiceCollection services)
    {
        services.AddScoped<IEnviaEmail, EnviaEmail>();
        services.AddScoped<IEmailTemplateProcessor, EmailTemplateProcessor>();
        services.AddScoped<EmailRedefinirSenha>();
    }
    public static void AddEncryption(IServiceCollection services, IConfiguration configuration)
    {
        var encryptKey = configuration.GetValue<string>("Keys:EncryptKey:Key");
        services.AddScoped<ICriptografarUseCase>(config => new CriptografarUseCase(encryptKey!));

        services.AddScoped<IDescriptografarUseCase>(config => new DescriptografarUseCase(encryptKey!));
    }

    public static void AddTokenGenerator(IServiceCollection services, IConfiguration configuration)
    {
        var accessTokenExpirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var refreshTokenExpirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:RefreshTokenExpiration");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:Signingkey");

        services.AddScoped<IAccessTokenGenerator>(config =>
        {
            var criptografarUseCase = config.GetRequiredService<ICriptografarUseCase>();
            var permissionsRepository = config.GetRequiredService<IPermissionsReadOnlyRepository>();

            return new JwtTokenGenerator(
                accessTokenExpirationTimeMinutes,
                signingKey!,
                refreshTokenExpirationTimeMinutes,
                criptografarUseCase,
                permissionsRepository);
        });

    }
}
