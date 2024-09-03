namespace DoseOfHope.Communication.Requests;

public class RequestUsuarioUpdateJson
{
    public string? nome { get; set; } = string.Empty;
    public string? email { get; set; } = string.Empty;
    public DateTime? dataNascimento { get; set; } = null;
}
