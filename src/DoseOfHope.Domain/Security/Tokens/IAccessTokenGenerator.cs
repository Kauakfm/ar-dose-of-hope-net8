using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    Task<Dictionary<string, string>> Generate(tabUsuario usuario);
}
