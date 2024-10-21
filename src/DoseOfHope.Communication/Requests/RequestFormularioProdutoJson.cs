using DoseOfHope.Communication.Enums;
using Microsoft.AspNetCore.Http;

namespace DoseOfHope.Communication.Requests;

public class RequestFormularioProdutoJson
{
    public TipoItemProduto tipoItem { get; set; }
    public string? nomeDoItem { get; set; }
    public FormaFarmaceutica formaFarmaceutica { get; set; }
    public TipoCondicao tipoCondicao { get; set; }
    public string? Dosagem { get; set; }
    public int quantidade { get; set; }
    public string? dataValidade { get; set; }
    public TipoNecessidadeArmazenamento necessidadeArmazenamento { get; set; }
    public string? descricaoDetalhada { get; set; }
    public List<IFormFile>? fotos { get; set; }

}
