using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabPermissions
{
    [Key]
    public int codigo { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }
}
