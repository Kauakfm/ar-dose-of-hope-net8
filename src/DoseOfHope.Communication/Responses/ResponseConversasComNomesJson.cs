namespace DoseOfHope.Communication.Responses;

public class ResponseConversasComNomesJson
{
    public int UsuarioCodigo { get; set; }
    public string RemetenteNome { get; set; } = string.Empty;
    public string? DestinatarioNome { get; set; }
    public string? Mensagem { get; set; }
    public string horaMinuto { get; set; } = string.Empty;


}
