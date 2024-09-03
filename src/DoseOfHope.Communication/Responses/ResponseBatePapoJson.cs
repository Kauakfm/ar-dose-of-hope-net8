using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Communication.Responses;

public class ResponseBatePapoJson
{
    public int codigo { get; set; }
    public string nome { get; set; }
    public List<tabProdutoDoado> doacao { get; set; }
    public int qtdItensDoados { get; set; }
    public string avatar { get; set; }
}