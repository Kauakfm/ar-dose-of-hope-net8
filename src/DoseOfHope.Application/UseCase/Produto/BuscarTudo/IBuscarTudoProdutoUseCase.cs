using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.Produto.BuscarTudo;

public interface IBuscarTudoProdutoUseCase
{
    Task<ResponseProdutosJson> GetProductsWithUserAndTypeProduct();
}
