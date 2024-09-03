using DoseOfHope.Application.Services.Context;
using DoseOfHope.Application.UseCase.BatePapo.BuscarTudo;
using DoseOfHope.Application.UseCase.BatePapo.SignalR;
using DoseOfHope.Application.UseCase.Login.FazerLogin;
using DoseOfHope.Application.UseCase.Produto.BuscarTudo;
using DoseOfHope.Application.UseCase.Usuario.Atualizar;
using DoseOfHope.Application.UseCase.Usuario.BucarTudo;
using DoseOfHope.Application.UseCase.Usuario.BuscarPorCodigo;
using DoseOfHope.Application.UseCase.Usuario.Deletar;
using DoseOfHope.Application.UseCase.Usuario.RedefinirSenha;
using DoseOfHope.Application.UseCase.Usuario.Registrar;
using DoseOfHope.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DoseOfHope.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCaseUsuario(services);
        AddUseCaseProduto(services);
        AddServices(services);
        AddUseCaseBatePapo(services);
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
    }
    private static void AddUseCaseBatePapo(IServiceCollection services)
    {
        services.AddScoped<IBuscarTudoBatePapoUseCase, BuscarTudoBatePapoUseCase>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IUserContextService, UserContextService>();
    }

}
