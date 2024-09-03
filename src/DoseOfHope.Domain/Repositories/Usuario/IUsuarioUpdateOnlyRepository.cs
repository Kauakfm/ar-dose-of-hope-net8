using DoseOfHope.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Domain.Repositories.Usuario
{
    public interface IUsuarioUpdateOnlyRepository
    {
        Task<tabUsuario?> GetByCodigo(int codigo);
        void Update(tabUsuario usuario);
    }
}
