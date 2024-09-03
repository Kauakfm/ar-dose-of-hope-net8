namespace DoseOfHope.Communication.Responses;

public class ResponseUsuarioJson
{
    public string codigo { get; set; }
    public string nome { get; set; }
    public string email { get; set; }
    public string cpf { get; set; }
    public DateTime? dataNascimento { get; set; }
    public string? foto { get; set; }
}
