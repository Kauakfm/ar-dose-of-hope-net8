using Amazon;
using Amazon.S3;
using DoseOfHope.Application.Services.Context;
using DoseOfHope.Application.UseCase.AmazonS3.BuscarImagem;
using DoseOfHope.Application.UseCase.AmazonS3.Registrar;
using DoseOfHope.Application.UseCase.BatePapo.BuscarTudo;
using DoseOfHope.Application.UseCase.Login.FazerLogin;
using DoseOfHope.Application.UseCase.Produto.BuscarPorCodigo;
using DoseOfHope.Application.UseCase.Produto.BuscarTudo;
using DoseOfHope.Application.UseCase.Produto.Registrar;
using DoseOfHope.Application.UseCase.Usuario.Atualizar;
using DoseOfHope.Application.UseCase.Usuario.BucarTudo;
using DoseOfHope.Application.UseCase.Usuario.BuscarPorCodigo;
using DoseOfHope.Application.UseCase.Usuario.Deletar;
using DoseOfHope.Application.UseCase.Usuario.RedefinirSenha;
using DoseOfHope.Application.UseCase.Usuario.Registrar;
using DoseOfHope.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoseOfHope.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCaseUsuario(services);
        AddUseCaseProduto(services);
        AddServices(services);
        AddUseCaseBatePapo(services);
        AddAmazonS3(services, configuration);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCaseUsuario(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
        services.AddScoped<IBuscarTudoUsuarioUseCase, BuscarTudoUsuarioUseCase>();
        services.AddScoped<IBuscarPorCodigoUsuarioUseCase, BuscarPorCodigoUsuarioUseCase>();
        services.AddScoped<IDeletarUsuarioUseCase, DeletarUsuarioUseCase>();
        services.AddScoped<IAtualizarUsuarioUseCase, AtualizarUsuarioUseCase>();
        services.AddScoped<IRedefinirSenhaUsuarioUseCase, RedefinirSenhaUsuarioUseCase>();
        services.AddScoped<IFazerLoginUseCase, FazerLoginUseCase>();
    }

    private static void AddUseCaseProduto(IServiceCollection services)
    {
        services.AddScoped<IBuscarTudoProdutoUseCase, BuscarTudoProdutoUseCase>();
        services.AddScoped<IRegistrarProdutoUseCase, RegistrarProdutoUseCase>();
        services.AddScoped<IBuscarPorCodigoProdutoUseCase, BuscarPorCodigoProdutoUseCase>();
    }
    private static void AddUseCaseBatePapo(IServiceCollection services)
    {
        services.AddScoped<IBuscarTudoBatePapoUseCase, BuscarTudoBatePapoUseCase>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();
    }
    private static void AddAmazonS3(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRegistrarAmazonS3UseCase, RegistrarAmazonS3UseCase>();
        services.AddScoped<IBuscarAmazonS3UseCase, BuscarAmazonS3UseCase>();

        var accessKey = configuration.GetValue<string>("Settings:AWS:AccessKeyS3");
        var secretKey = configuration.GetValue<string>("Settings:AWS:SecretKeyS3");

        services.AddScoped<IAmazonS3>(provider =>
        {
            return new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        });
    }

}
