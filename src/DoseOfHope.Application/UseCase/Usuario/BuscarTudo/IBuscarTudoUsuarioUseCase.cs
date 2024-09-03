using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Application.UseCase.Usuario.BucarTudo
{
    public interface IBuscarTudoUsuarioUseCase
    {
        Task<ResponseUsuariosJson> Execute();
    }
}
