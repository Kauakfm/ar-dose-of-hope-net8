using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Domain.Entities;

public class tabProdutoDoadoImagem
{
    [Key]
    public long codigo { get; set; }
    public int produtoCodigo { get; set; }
    public string? nomeImagem { get; set; }
}
