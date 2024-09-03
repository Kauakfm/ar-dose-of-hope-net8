using System.Net;
using System.Net.Mail;

namespace DoseOfHope.Infrastructure.Email.EnviaEmailAsync;

public class EnviaEmail : IEnviaEmail
{
    public async Task EnviaEmailAsync(string nomeRemetente, string emailDestinatario, string assuntoMensagem, string conteudoMensagem)
    {
        var porta = 587;
        var smtp = "smtp.titan.email";
        var isSSL = false;
        var usuario = "kaua.martins@varsolutions.com.br";
        var senha = "Var@1234";

        var objEmail = new MailMessage(usuario, emailDestinatario, assuntoMensagem, conteudoMensagem);

        objEmail.From = new MailAddress(nomeRemetente + "<" + usuario + ">");
        objEmail.IsBodyHtml = true;
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
        objEmail.Subject = assuntoMensagem;
        objEmail.Body = conteudoMensagem;

        using (var objSmtp = new SmtpClient(smtp, porta))
        {
            objSmtp.EnableSsl = isSSL;
            objSmtp.UseDefaultCredentials = false;
            objSmtp.Credentials = new NetworkCredential(usuario, senha);

            try
            {
               objSmtp.Send(objEmail);
            }
            finally
            {
                objEmail.Dispose();
            }
        }
    }
}
