using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.Usuario;

public interface IUsuariosReadOnlyRepository
{
    Task<List<tabUsuario>> GetAll();
    Task<tabUsuario?> GetByCodigo(int codigo);
    Task<tabUsuario?> GetByEmail(string email);
    Task<List<tabUsuario>> GetAllUsersAndTypeUser();
    Task<tabUsuario?> GetUserbyEmailAndPassword(string email, string password);
    Task<List<tabUsuario>> GetUserWithRoleTypeUser();
    Task<List<tabUsuario>> GetUsersWithDonationsAndUserTypeRoleUser();
}
