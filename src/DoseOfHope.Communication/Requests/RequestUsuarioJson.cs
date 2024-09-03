using DoseOfHope.Communication.Enums;

namespace DoseOfHope.Communication.Requests;

public class RequestUsuarioJson
{
    public string nome { get; set; }
    public string email { get; set; }
    public string senha { get; set; }
    public DateTime? dataNascimento { get; set; }
    public string cpf { get; set; }
    public int? tipoUsuarioCodigo { get; set; }
    public DateTime? dataEmailEnviado { get; set; }
    public DateTime? dataCriacao { get; set; }
    public TipoUnidade unidadeCodigo { get; set; }

}
