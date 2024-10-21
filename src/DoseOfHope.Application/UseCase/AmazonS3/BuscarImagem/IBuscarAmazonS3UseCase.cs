using DoseOfHope.Communication.Requests;

namespace DoseOfHope.Application.UseCase.AmazonS3.BuscarImagem;

public interface IBuscarAmazonS3UseCase
{
    Task<string> GetUrlFile(RequestAmazonS3GetJson request);
}
