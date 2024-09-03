using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabMensagens
{
    [Key]
    public int codigo { get; set; }
    public int conversaCodigo { get; set; }
    public int RementeUsuarioCodigo { get; set; }
    public int DestinatarioUsuarioCodigo { get; set; }
    public string Conteudo { get; set; }

    public DateTime dataEnvio { get; set; }

}
