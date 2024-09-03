using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabStatus
{
    [Key]
    public int codigo { get; set; }

    public string descricao { get; set; }
}
