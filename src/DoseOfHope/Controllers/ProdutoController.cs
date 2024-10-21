using DoseOfHope.Application.Services.Context;
using DoseOfHope.Application.UseCase.Produto.BuscarTudo;
using DoseOfHope.Application.UseCase.Produto.Registrar;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> InserirProdutoDoado([FromServices] IRegistrarProdutoUseCase useCase,
        [FromForm] RequestFormularioProdutoJson request,
        [FromServices] IUserContextService userCodeContext)
    {
        var userCode = userCodeContext.GetUserCode();

        var response = await useCase.Executar(request, userCode);

        return Created("", response);
    }
    [HttpGet]
    [Route("ListarTodasDoacoes")]
    [Authorize]
    public async Task<IActionResult> ListarTodasAsDoacoesProdutosDoados([FromServices] IBuscarTudoProdutoUseCase useCase,[FromServices] IUserContextService userCodeContext)
    {
        var userCode = userCodeContext.GetUserCode();

        var response = await useCase.ListarTodasAsDoacoesProdutosDoados(userCode);

        return Ok(response);
    }

}
