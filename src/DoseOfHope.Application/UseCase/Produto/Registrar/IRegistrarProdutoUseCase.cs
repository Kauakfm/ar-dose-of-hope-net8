using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.Produto.Registrar
{
    public interface IRegistrarProdutoUseCase
    {
        Task<ResponseRegistrarProdutoJson> Executar(RequestFormularioProdutoJson request, int usuarioCodigo);
    }
}
