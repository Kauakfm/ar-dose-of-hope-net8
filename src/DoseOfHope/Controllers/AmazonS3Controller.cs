using DoseOfHope.Application.UseCase.AmazonS3.BuscarImagem;
using DoseOfHope.Application.UseCase.AmazonS3.Registrar;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoseOfHope.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AmazonS3Controller : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SalvarArquivoS3([FromForm] RequestAmazonS3UploadJson request, 
        [FromServices] IRegistrarAmazonS3UseCase useCase)
    {
        var response = await useCase.SalvarArquivoS3(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> BuscarURLArquivoS3([FromQuery] RequestAmazonS3GetJson request,
        [FromServices] IBuscarAmazonS3UseCase useCase)
    {
        var response = await useCase.GetUrlFile(request);

        return Ok(response);
    }
}
