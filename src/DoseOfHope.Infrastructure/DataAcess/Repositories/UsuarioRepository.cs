using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories.Usuario;
using Microsoft.EntityFrameworkCore;

namespace DoseOfHope.Infrastructure.DataAcess.Repositories;

internal class UsuarioRepository : IUsuariosWriteOnlyRepository, IUsuariosReadOnlyRepository, IUsuarioUpdateOnlyRepository
{
    private readonly DoseOfHopeDbContext _dbContext;
    public UsuarioRepository(DoseOfHopeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(tabUsuario usuario)
    {
        await _dbContext.tabUsuario.AddAsync(usuario);
    }
    public async Task AddRoles(tabUsuario_tabRoles roles)
    {
        await _dbContext.tabUsuario_tabRoles.AddAsync(roles);  
    }

    public async Task<bool> Delete(int codigo)
    {
        var user = await _dbContext.tabUsuario.FirstOrDefaultAsync(usuario => usuario.codigo == codigo);

        if (user is null)
            return false;

        _dbContext.tabUsuario.Remove(user);
        return true;
    }

    public async Task<List<tabUsuario>> GetAll()
    {
        return await _dbContext.tabUsuario.ToListAsync();
    }

    async Task<tabUsuario?> IUsuariosReadOnlyRepository.GetByCodigo(int codigo)
    {
        return await _dbContext.tabUsuario.FirstOrDefaultAsync(usuario => usuario.codigo == codigo);
    }

    async Task<tabUsuario?> IUsuarioUpdateOnlyRepository.GetByCodigo(int codigo)
    {
        return await _dbContext.tabUsuario.Include(tipoUsuario => tipoUsuario.TipoUsuarioCodigo).FirstOrDefaultAsync(usuario => usuario.codigo == codigo);
    }

    public void Update(tabUsuario usuario)
    {
        _dbContext.tabUsuario.Update(usuario);
    }

    public async Task<tabUsuario?> GetByEmail(string email)
    {
        return await _dbContext.tabUsuario.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.email.ToUpper() == email.ToUpper());
    }

    public async Task<List<tabUsuario>> GetAllUsersAndTypeUser()
    {
        var users = await _dbContext.tabUsuario.AsNoTracking()
             .Include(tipoUsuario => tipoUsuario.TipoUsuarioCodigo)
             .ToListAsync();

        return users;
    }

    public async Task<tabUsuario?> GetUserbyEmailAndPassword(string email, string password)
    {
        var user = await _dbContext.tabUsuario.AsNoTracking().FirstOrDefaultAsync(user => user.email.ToLower() == email.ToLower() && user.senha == password);

        if (user is null)
            return null;

        return user;
    }

    public async Task<List<tabUsuario>> GetUserWithRoleTypeUser()
    {
        var roleTypeUser = await _dbContext.tabUsuario_tabRoles.Where(role => role.roleCodigo == 2).Select(r => r.usuarioCodigo).ToListAsync();

        return await _dbContext.tabUsuario.Where(user => roleTypeUser.Contains(user.codigo)).ToListAsync();
    }

    public async Task<List<tabUsuario>> GetUsersWithDonationsAndUserTypeRoleUser()
    {
        var roleTypeUserCodes = await _dbContext.tabUsuario_tabRoles.Where(role => role.roleCodigo == 2).Select(r => r.usuarioCodigo).ToListAsync();

        var users = await _dbContext.tabUsuario.Where(user => roleTypeUserCodes.Contains(user.codigo) &&
        _dbContext.tabProdutoDoado.Any(product => product.usuarioCodigo == user.codigo))
            .Select(x => new tabUsuario
            {
                codigo = x.codigo,
                nome = x.nome,
                foto = string.IsNullOrEmpty(x.foto) ? "https://api.dicebear.com/8.x/bottts-neutral/svg?seed=Max" : x.foto
            }).ToListAsync();

        return users;
    }

    public async Task<bool> EmailExisteAsync(string email)
    {
        return await _dbContext.tabUsuario.AsNoTracking().AnyAsync(usuario => usuario.email.ToUpper() == email.ToUpper());
    }

    public async Task<bool> CpfExisteAsync(string cpf)
    {
        return await _dbContext.tabUsuario.AsNoTracking().AnyAsync(usuario => usuario.cpf == cpf);
    }
}
