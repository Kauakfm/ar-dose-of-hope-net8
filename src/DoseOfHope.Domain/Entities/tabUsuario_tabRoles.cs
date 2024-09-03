using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabUsuario_tabRoles
{
    [Key]
    public  int usuarioCodigo { get; set; }

    public int roleCodigo { get; set; }
}
