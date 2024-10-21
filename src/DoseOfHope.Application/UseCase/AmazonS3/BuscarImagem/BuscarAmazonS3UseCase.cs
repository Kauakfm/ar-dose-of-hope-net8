using Amazon.S3;
using Amazon.S3.Model;
using DoseOfHope.Communication.Requests;

namespace DoseOfHope.Application.UseCase.AmazonS3.BuscarImagem;

public class BuscarAmazonS3UseCase : IBuscarAmazonS3UseCase
{
    private readonly string _bucketName = "doseofhopeproduction";
    private readonly IAmazonS3 _clienteS3;

    public BuscarAmazonS3UseCase(IAmazonS3 s3Client)
    {
        _clienteS3 = s3Client;
    }

    public async Task<string> GetUrlFile(RequestAmazonS3GetJson request)
    {
        var objReq = new GetPreSignedUrlRequest()
        {
            BucketName = _bucketName,
            Key = $"{request.directoryName}/{request.fileName}",
            Expires = DateTime.UtcNow.AddHours(24),
        };
        return await _clienteS3.GetPreSignedURLAsync(objReq);
    }
}
