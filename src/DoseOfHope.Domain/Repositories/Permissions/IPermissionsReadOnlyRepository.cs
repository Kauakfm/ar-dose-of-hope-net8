using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.Permissions;

public interface IPermissionsReadOnlyRepository
{
    Task<List<tabPermissions>> GetPermissionsUsers(int codigo);
}
