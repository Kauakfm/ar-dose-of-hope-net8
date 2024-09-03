using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Infrastructure.Email.EnviaEmailAsync
{
    public interface IEnviaEmail
    {
        Task EnviaEmailAsync(string nomeRemetente, string emailDestinatario, string assuntoMensagem, string conteudoMensagem);
    }
}
