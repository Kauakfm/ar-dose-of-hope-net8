using DoseOfHope.Application.Services.Context;
using DoseOfHope.Application.UseCase.Login.FazerLogin;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoseOfHope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserContextService _userContextService;
        public LoginController(IUserContextService userContextService)
        {
            _userContextService = userContextService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> Login(
            [FromServices] IFazerLoginUseCase useCase,
            [FromBody] RequestLoginJson request)
        {
            var response = await useCase.Execute(request);

            return Ok(response);
        }

        [HttpPost("refresh-login")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> LoginSemSenha([FromServices] IFazerLoginUseCase useCase)
        {
            var identidade = HttpContext.User.Identity as ClaimsIdentity;
            var usuarioCodigo = identidade?.FindFirst(ClaimTypes.Sid)?.Value;

            var response = await useCase.LoginSemSenha(Convert.ToInt32(usuarioCodigo));
            return Ok(response);
        }
    }
}
