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
    public string? nome { get; set; }
    public int? qntd { get; set; }
    public DateTime? validade { get; set; }
    public int? formaFarmaceuticaCodigo { get; set; }
    [ForeignKey("formaFarmaceuticaCodigo")]
    public tabFormaFarmaceutica FormaFarmaceutica { get; set; }
    public string? dosagemEscrita { get; set; }
    public string? validadeEscrita { get; set; }
    public string? descricaoDetalhada { get; set; }
    public int? tipoCondicaoCodigo { get; set; }
    [ForeignKey("tipoCondicaoCodigo")]
    public tabTipoCondicao TipoCondicao { get; set; }
    public int? tipoNecessidadeArmazenamentoCodigo { get; set; }
    [ForeignKey("tipoNecessidadeArmazenamentoCodigo")]
    public tabTipoNecessidadeArmazenamento TipoNecessidadeArmazenamento { get; set; }

}
