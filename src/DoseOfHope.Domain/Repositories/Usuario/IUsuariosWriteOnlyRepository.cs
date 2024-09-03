using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.Usuario
{
    public interface IUsuariosWriteOnlyRepository
    {
        Task Add(tabUsuario usuario);
        Task AddRoles(tabUsuario_tabRoles roles);

        Task<bool> Delete(int codigo);
    }
}
