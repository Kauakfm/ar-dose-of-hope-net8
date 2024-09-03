using DoseOfHope.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoseOfHope.Infrastructure.DataAcess;

internal class DoseOfHopeDbContext : DbContext
{
    public DoseOfHopeDbContext(DbContextOptions options) : base(options) { }
    public DbSet<tabUsuario> tabUsuario { get; set; }
    public DbSet<tabProdutoDoado> tabProdutoDoado { get; set; }
    public DbSet<tabTipoProduto> tabTipoProduto { get; set; }
    public DbSet<tabStatus> tabStatus { get; set; }
    public DbSet<tabTipoUsuario> tabTipoUsuario { get; set; }
    public DbSet<tabRoles> tabRoles { get; set; }
    public DbSet<tabPermissions> tabPermissions { get; set; }
    public DbSet<tabUsuario_tabRoles> tabUsuario_tabRoles { get; set; }
    public DbSet<tabRole_tabPermissions> tabRole_tabPermissions { get; set; }
    public DbSet<tabMensagens> tabMensagens { get; set; }
    public DbSet<tabConversas> tabConversas { get; set; }
   
}
