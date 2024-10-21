using Amazon.S3;
using Amazon.S3.Model;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Exception.ExceptionBase;

namespace DoseOfHope.Application.UseCase.AmazonS3.Registrar;

public class RegistrarAmazonS3UseCase : IRegistrarAmazonS3UseCase
{
    private readonly string _bucketName = "doseofhopeproduction";
    private readonly IAmazonS3 _clienteS3;

    public RegistrarAmazonS3UseCase(IAmazonS3 s3Client)
    {
        _clienteS3 = s3Client;
    }

    public async Task<string> SalvarArquivoS3(RequestAmazonS3UploadJson request)
    {
        var bucketExiste = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_clienteS3, _bucketName);

        if (!bucketExiste)
            throw new NotFoundException($"Bucket {_bucketName} não existe");

        if (request.arquivo is null)
            throw new ArgumentException("Faça upload de pelo menos um arquivo");

        var newName = $"{Guid.NewGuid()}_{request.diretorio}_{request.arquivo.FileName}";

        var requisicao = new PutObjectRequest()
        {
            BucketName = _bucketName,
            Key = $"{request.diretorio}/{newName}",
            InputStream = request.arquivo?.OpenReadStream()
        };
        requisicao.Metadata.Add("Content-Type", request.arquivo?.ContentType);
        await _clienteS3.PutObjectAsync(requisicao);

        return newName;
    }


}
