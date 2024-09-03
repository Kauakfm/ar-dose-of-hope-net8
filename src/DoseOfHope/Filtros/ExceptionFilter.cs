using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DoseOfHope.Exception.ExceptionBase;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Exception;

namespace DoseOfHope.Filtros;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DoseOfHopeException)
        {
            LidaComExcecaoDoProjeto(context);
        }
        else
        {
            ErroDesconhecido(context);
        }
    }

    private void LidaComExcecaoDoProjeto(ExceptionContext context)
    {
        var doseOfHopeException = context.Exception as DoseOfHopeException;
        var errorResponse = new ResponseErrorJson(doseOfHopeException!.GetErrors());


        context.HttpContext.Response.StatusCode = doseOfHopeException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ErroDesconhecido(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(ResourceErrorMessages.ERRO_DESCONHECIDO);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }


    /*if (context.Exception is ErrorOnValidationException ex)
      {
          var errorResponse = new ResponseErrorJson(ex.Errors);

          context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
          context.Result = new BadRequestObjectResult(errorResponse);
      }
      else if (context.Exception is NotFoundException notFoundException)
      {
          var errorResponse = new ResponseErrorJson(notFoundException.Message);

          context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
          context.Result = new BadRequestObjectResult(errorResponse);
      }
      else
      {
          var errorResponse = new ResponseErrorJson(context.Exception.Message);

          context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
          context.Result = new BadRequestObjectResult(errorResponse);
      }
      */
}