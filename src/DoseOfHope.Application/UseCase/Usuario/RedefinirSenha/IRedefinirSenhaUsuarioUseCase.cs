using DoseOfHope.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Application.UseCase.Usuario.RedefinirSenha
{
    public interface IRedefinirSenhaUsuarioUseCase
    {
        Task RedefinirSenha(string token, RequestRedefinirSenhaJson request);
        Task EnviarEmailDeRedefinirSenha(RequestEmailJson request);
    }
}
