namespace DoseOfHope.Communication.Responses;

public class ResponseConversaComNomeJson
{
    public int UsuarioCodigo { get; set; }
    public string RemetenteNome { get; set; }

    public string? DestinatarioNome { get; set; }

    public string? Mensagem { get; set; }

    public string horaMinuto { get; set; }

}
