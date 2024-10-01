using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoseOfHope.Domain.Entities;

public class tabProdutoDoado
{
    [Key]
    public int codigo { get; set; }

    public int usuarioCodigo { get; set; }
    [ForeignKey("usuarioCodigo")]
    public tabUsuario Usuario { get; set; }

    public int tipoProdutoCodigo { get; set; }
    [ForeignKey("tipoProdutoCodigo")]
    public tabTipoProduto TipoProduto { get; set; }

    public int codigoStatus { get; set; }
    [ForeignKey("codigoStatus")]
    public tabStatus statusCodigo { get; set; }

    public int? forma { get; set; }
    public string? nome { get; set; }
    public int? dosagem { get; set; }
    public int? qntd { get; set; }
    public DateTime? validade { get; set; }
    public int? formaFarmaceuticaCodigo { get; set; }
    public int? adulto_pediatrico { get; set; }
    public string? dosagemEscrita { get; set; }
    public string? validadeEscrita { get; set; }
    public int? necessidadeArmazenamento { get; set; }
    public string? descricaoDetalhada { get; set; }
    public long? produtoImagemCodigo { get; set; }
}
