using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Communication.Responses;

public class ResponseShortDoacaoProdutoJson
{
    public int codigo { get; set; }
    public string? tipoProdutoDescricao { get; set; }
    public string? nomeDoItem { get; set; }
    public string? formaFarmaceuticaDescricao { get; set; }
    public string? tipoCondicaoDescricao { get; set; }
    public string? dosagemEscrita { get; set; }
    public int quantidade { get; set; }
    public string? validadeEscrita { get; set; }
    public string? tipoNecessidadeArmazenamentoDescricao { get; set; }
    public string? descricaoDetalhada { get; set; }
    public List<string?> urlImages { get; set; }

}
