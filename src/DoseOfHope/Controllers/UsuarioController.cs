using DoseOfHope.Application.Services.Context;
using DoseOfHope.Application.UseCase.Usuario.Atualizar;
using DoseOfHope.Application.UseCase.Usuario.BucarTudo;
using DoseOfHope.Application.UseCase.Usuario.BuscarPorCodigo;
using DoseOfHope.Application.UseCase.Usuario.Deletar;
using DoseOfHope.Application.UseCase.Usuario.RedefinirSenha;
using DoseOfHope.Application.UseCase.Usuario.Registrar;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoseOfHope.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUserContextService _userContextService;
    public UsuarioController(IUserContextService userContextService)
    {
        _userContextService = userContextService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase,
        [FromBody] RequestUsuarioJson request)
    {
        var response = await useCase.Executar(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUsuariosJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BuscarTodosUsuarios([FromServices] IBuscarTudoUsuarioUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Usuarios.Count() != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [Route("BuscarPorCodigo")]
    [ProducesResponseType(typeof(ResponseUsuarioJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorCodigo([FromServices] IBuscarPorCodigoUsuarioUseCase useCase)
    {
        var usuarioCodigo = _userContextService.GetUserCode();

        var response = await useCase.Execute(usuarioCodigo);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{codigo}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeletarUsuarioUseCase useCase, [FromRoute] int codigo)
    {
        await useCase.Execute(codigo);

        return NoContent();
    }
    [HttpPut]
    [Authorize]
    [Route("AtualizarPerfilUsuario")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IAtualizarUsuarioUseCase useCase, [FromBody] RequestUsuarioUpdateJson request)
    {
        var usuarioCodigo = _userContextService.GetUserCode();

        await useCase.Execute(usuarioCodigo, request);

        return NoContent();
    }

    [HttpPut]
    [Route("EsqueciSenha/{codigo}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RedefinirSenha([FromServices] IRedefinirSenhaUsuarioUseCase useCase, [FromRoute] string codigo, [FromBody] RequestRedefinirSenhaJson request)
    {
        await useCase.RedefinirSenha(codigo, request);

        return NoContent();
    }

    [HttpPost]
    [Route("EnviarEmailDeRedefinirSenha")]
    [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EnviarEmailDeRedefinirSenha([FromServices] IRedefinirSenhaUsuarioUseCase useCase, [FromBody] RequestEmailJson request)
    {
        await useCase.EnviarEmailDeRedefinirSenha(request);

        return Ok();
    }

    [HttpPut]
    [Authorize]
    [Route("AtualizarAvatarUsuario")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarAvatarUsuario([FromServices] IAtualizarUsuarioUseCase useCase,[FromBody] RequestUsuarioAvatarJson request)
    {
        var usuarioCodigo = _userContextService.GetUserCode();

        await useCase.AtualizarAvatarUsuario(usuarioCodigo, request);

        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [Route("BuscarAvatar")]
    [ProducesResponseType(typeof(ResponseFotoUsuarioJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarAvatar([FromServices] IBuscarPorCodigoUsuarioUseCase useCase)
    {
        var usuarioCodigo = _userContextService.GetUserCode();

        var response = await useCase.BuscarAvatar(usuarioCodigo);
        return Ok(response);
    }  
}

