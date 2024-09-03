using DoseOfHope.Application.UseCase.Produto.BuscarTudo;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DoseOfHope.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseProdutosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscarProdutosComUsuarioETipoProduto([FromServices] IBuscarTudoProdutoUseCase useCase)
    {
        //var identidade = (ClaimsIdentity)HttpContext.User.Identity;
        //var usuarioCodigo = identidade.FindFirst("TipoUsuarioCodigo").Value;

        var response = await useCase.GetProductsWithUserAndTypeProduct();

        if (response.Produtos.Count() != 0)
            return Ok(response);

        return NoContent();
    }

}
