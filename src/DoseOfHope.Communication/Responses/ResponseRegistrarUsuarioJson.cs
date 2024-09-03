namespace DoseOfHope.Communication.Responses;

public class ResponseRegistrarUsuarioJson
{
    public  string Nome { get; set; } = string.Empty;
    public  string Accesstoken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int TipoUsurioCodigo { get; set; }
    public string UsuarioCodigo { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
}
