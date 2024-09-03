using DoseOfHope.Application.UseCase.BatePapo.BuscarTudo;
using DoseOfHope.Application.UseCase.Usuario.BucarTudo;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoseOfHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatePapoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseBatePapoJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> BuscarTodosUsuarioComDoacao([FromServices] IBuscarTudoBatePapoUseCase useCase)
        {
            var response = await useCase.BuscarTodosBatePapos();

            if (response.Count() != 0)
                return Ok(response);

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("{remetenteUsuarioCodigo}/{destinatarioUsuarioCodigo}")]
        [ProducesResponseType(typeof(ResponseMensagensComCodigoConversaJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarTodosUsuarioComDoacao([FromServices] IBuscarTudoBatePapoUseCase useCase,
            [FromRoute] int remetenteUsuarioCodigo, [FromRoute] int destinatarioUsuarioCodigo)
        {
            var response = await useCase.BuscarConversa_BuscarMensagensConversa(remetenteUsuarioCodigo, destinatarioUsuarioCodigo);

            return Ok(response);
        }

    }
}
