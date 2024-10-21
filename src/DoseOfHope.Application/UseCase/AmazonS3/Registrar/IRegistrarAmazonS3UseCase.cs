using DoseOfHope.Communication.Requests;

namespace DoseOfHope.Application.UseCase.AmazonS3.Registrar;

public interface IRegistrarAmazonS3UseCase
{
    Task<string> SalvarArquivoS3(RequestAmazonS3UploadJson request);

}
