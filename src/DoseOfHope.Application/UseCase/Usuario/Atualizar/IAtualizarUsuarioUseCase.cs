using DoseOfHope.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Application.UseCase.Usuario.Atualizar
{
    public interface IAtualizarUsuarioUseCase
    {
        Task Execute(int codigo, RequestUsuarioUpdateJson request);
        Task AtualizarAvatarUsuario(int codigo, RequestUsuarioAvatarJson request);
    }
}
