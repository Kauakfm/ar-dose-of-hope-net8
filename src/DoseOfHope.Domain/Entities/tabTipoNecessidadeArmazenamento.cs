using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabTipoNecessidadeArmazenamento
{
    [Key]
    public int Codigo { get; set; }
    public string Descricao { get; set; }
}
