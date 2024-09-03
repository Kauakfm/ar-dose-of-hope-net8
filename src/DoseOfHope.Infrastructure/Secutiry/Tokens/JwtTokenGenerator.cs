using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories.Permissions;
using DoseOfHope.Domain.Security.Tokens;
using DoseOfHope.Infrastructure.Encryption.Criptografar;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoseOfHope.Infrastructure.Secutiry.Tokens;

internal class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _accessTokenExpirationTimeMinutes;
    private readonly string _signingKey;
    private readonly uint _refreshTokenExpirationTimeMinutes;
    private readonly ICriptografarUseCase _criptografarUseCase;
    private readonly IPermissionsReadOnlyRepository _permissionsReadOnlyRepository;

    public JwtTokenGenerator(uint accessTokenExpirationTimeMinutes, string signingKey, uint refreshTokenExpirationTimeMinutes, ICriptografarUseCase criptografarUseCase, IPermissionsReadOnlyRepository permissionsReadOnlyRepository)
    {
        _accessTokenExpirationTimeMinutes = accessTokenExpirationTimeMinutes;
        _signingKey = signingKey;
        _refreshTokenExpirationTimeMinutes = refreshTokenExpirationTimeMinutes;
        _criptografarUseCase = criptografarUseCase;
        _permissionsReadOnlyRepository = permissionsReadOnlyRepository;
    }

    public async Task<Dictionary<string, string>> Generate(tabUsuario usuario)
    {
        var accessTokenClaims = await GerarClaims(usuario);
        var refreshTokenClaims = await GerarClaims(usuario, true);

        var acessToken = GerarTokens(accessTokenClaims, _accessTokenExpirationTimeMinutes);
        var refreshToken = GerarTokens(refreshTokenClaims, _refreshTokenExpirationTimeMinutes);

        return new Dictionary<string, string>()
        {
            {"accessToken", acessToken },
            {"refreshToken", refreshToken },
        };
    }
    private string GerarTokens(List<Claim> claims, uint expirationTimeMinutes)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingKey);
        return new SymmetricSecurityKey(key);
    }

    private async Task<List<Claim>> GerarClaims(tabUsuario usuario, bool isRefresh = false)
    {
        var claims = new List<Claim>();

        var permissions = await _permissionsReadOnlyRepository.GetPermissionsUsers(usuario.codigo);

        if (isRefresh)
        {
            claims.Add(new Claim(ClaimTypes.Sid, usuario.codigo.ToString()));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Name, usuario.nome));
            claims.Add(new Claim(ClaimTypes.Sid, _criptografarUseCase.Encrypt(usuario.codigo.ToString())));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(",", permissions.Select(p => p.nome))));
            claims.Add(new Claim("avatar", usuario.foto!));
            claims.Add(new Claim("tipoUsuarioCodigo", usuario.tipoUsuarioCodigo.ToString()));
        }
        return claims;
    }
}
