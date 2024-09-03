using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabRoles
{
    [Key]
    public int codigo { get; set; }

    public string descricao { get; set; }

}
