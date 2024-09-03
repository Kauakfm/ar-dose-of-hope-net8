using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Application.UseCase.Usuario.Deletar
{
    public interface IDeletarUsuarioUseCase
    {
        Task Execute(int codigo);
    }
}
