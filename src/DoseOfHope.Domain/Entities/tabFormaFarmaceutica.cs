using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabFormaFarmaceutica
{
    [Key]
    public int Codigo { get; set; }
    public string Descricao { get; set; }
}
