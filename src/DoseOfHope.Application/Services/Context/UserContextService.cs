using DoseOfHope.Infrastructure.Encryption.Descriptografar;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DoseOfHope.Application.Services.Context;

public class UserContextService : IUserContextService
{
    private readonly HttpContext _httpContext;
    private readonly IDescriptografarUseCase _decryptUseCase;

    public UserContextService(IHttpContextAccessor httpContextAccessor, IDescriptografarUseCase decryptService)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _decryptUseCase = decryptService;
    }

    public int GetUserCode()
    {
        var identidade = _httpContext.User.Identity as ClaimsIdentity;
        var usuarioCodigo = identidade?.FindFirst(ClaimTypes.Sid)?.Value;

        if (usuarioCodigo is null)
            throw new ArgumentException("Código do usuário não encontrado nos claims.");
        

        return Convert.ToInt32(_decryptUseCase.Decrypt(usuarioCodigo!));
    }

    public int GetTypeCode()
    {
        var identidade = _httpContext.User.Identity as ClaimsIdentity;
        var tipoUsuario = identidade?.FindFirst("TipoUsuarioCodigo")?.Value;

        if (tipoUsuario is null)
            throw new ArgumentException("Tipo de usuário não encontrado nos claims.");


        return Convert.ToInt32(tipoUsuario);
    }
}
