using Microsoft.AspNetCore.Http;

namespace DoseOfHope.Communication.Requests;

public class RequestAmazonS3UploadJson
{
    public IFormFile? arquivo { get; set; }

    public string? prefixo { get; set; }

    public string? diretorio { get; set; }

}
