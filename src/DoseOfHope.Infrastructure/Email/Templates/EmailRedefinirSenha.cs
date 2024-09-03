using DoseOfHope.Infrastructure.Encryption.Criptografar;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Infrastructure.Email.EnviaEmailAsync;

namespace DoseOfHope.Infrastructure.Email.Templates;

public class EmailRedefinirSenha
{
    private readonly IEmailTemplateProcessor _templateProcessor;
    private readonly IEnviaEmail _enviaEmail;
    private readonly ICriptografarUseCase _criptografarUseCase;

    public EmailRedefinirSenha(IEmailTemplateProcessor emailTemplateProcessor,
         IEnviaEmail enviaEmail,
         ICriptografarUseCase criptografarUseCase)
    {
        _templateProcessor = emailTemplateProcessor;
        _enviaEmail = enviaEmail;   
        _criptografarUseCase = criptografarUseCase;
    }
    public async Task EnviaEmail(tabUsuario objUser)
    {
        var placeholders = new Dictionary<string, string>
            {
                 { "@codigo", _criptografarUseCase.CriptografarID(objUser.codigo) },
                 { "@nome", objUser.nome.Split(" ")[0] }
            };

        var template = _templateProcessor.LoadTemplate("RedefinirSenha");
        var processedHtml = _templateProcessor.ProcessTemplate(template, placeholders);

        await _enviaEmail.EnviaEmailAsync("Dose de esperança", objUser.email, "Redefinição de senha", processedHtml);
    }
}
