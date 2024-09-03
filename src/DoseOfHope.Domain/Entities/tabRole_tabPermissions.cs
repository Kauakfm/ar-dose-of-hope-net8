using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabRole_tabPermissions
{
    [Key]
    public int roleCodigo { get; set; }
    public int permissionCodigo { get; set; }
}
