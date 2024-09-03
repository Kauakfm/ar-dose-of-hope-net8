using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories.Permissions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoseOfHope.Infrastructure.DataAcess.Repositories;

internal class PermissionRespository : IPermissionsReadOnlyRepository
{
    private readonly DoseOfHopeDbContext _dbContext;

    public PermissionRespository(DoseOfHopeDbContext doseOfHopeDbContext)
    {
        _dbContext = doseOfHopeDbContext;
    }

    public async Task<List<tabPermissions>> GetPermissionsUsers(int codigo)
    {
        var userRole = await _dbContext.tabUsuario_tabRoles.FirstOrDefaultAsync(ur => ur.usuarioCodigo == codigo);

        var rolePermissions = await _dbContext.tabRole_tabPermissions.Where(rp => rp.roleCodigo == userRole!.roleCodigo).Select(rp => rp.permissionCodigo).ToListAsync();

        var lstPermissions = await _dbContext.tabPermissions.Where(p => rolePermissions.Contains(p.codigo)).ToListAsync();

        return lstPermissions;
    }
}

