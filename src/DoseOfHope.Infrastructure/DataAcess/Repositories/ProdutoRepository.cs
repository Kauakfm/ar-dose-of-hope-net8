using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories.Produto;
using Microsoft.EntityFrameworkCore;

namespace DoseOfHope.Infrastructure.DataAcess.Repositories
{
    internal class ProdutoRepository : IProdutoReadOnlyRepository, IProdutoWriteOnlyRepository, IProdutoUpdateOnlyRepository
    {
        private readonly DoseOfHopeDbContext _dbContext;

        public ProdutoRepository(DoseOfHopeDbContext doseOfHopeDbContext)
        {
            _dbContext = doseOfHopeDbContext;
        }

        public async Task Add(tabProdutoDoado produto)
        {
            await _dbContext.tabProdutoDoado.AddAsync(produto);
        }
        public async Task<bool> Delete(int codigo)
        {
            var produto = await _dbContext.tabProdutoDoado.FirstOrDefaultAsync(produto => produto.codigo == codigo);

            if (produto is null)
                return false;

            _dbContext.tabProdutoDoado.Remove(produto);
            return true;
        }

        public async Task<List<tabProdutoDoado>> GetAll()
        {
            return await _dbContext.tabProdutoDoado.AsNoTracking().ToListAsync();
        }

        async Task<tabProdutoDoado?> IProdutoReadOnlyRepository.GetByCodigo(int codigo)
        {
            return await _dbContext.tabProdutoDoado.AsNoTracking().FirstOrDefaultAsync(produto => produto.codigo == codigo);
        }

        async Task<tabProdutoDoado?> IProdutoUpdateOnlyRepository.GetByCodigo(int codigo)
        {
            return await _dbContext.tabProdutoDoado.FirstOrDefaultAsync(produto => produto.codigo == codigo);
        }

        public void Update(tabProdutoDoado produto)
        {
            _dbContext.tabProdutoDoado.Update(produto);
        }

        public async Task<List<tabProdutoDoado>> GetProductsWithUserAndTypeProduct()
        {
            var product = await _dbContext.tabProdutoDoado.AsNoTracking()
                .Include(usuario => usuario.Usuario)
                .Include(tipoProduto => tipoProduto.TipoProduto)
                .ToListAsync();

            return product;
        }
             
        public async Task<List<tabProdutoDoado>> GetUsersWhatDonated(List<int> userCodes)
        {
            return await _dbContext.tabProdutoDoado.Where(d => userCodes.Contains(d.usuarioCodigo)).ToListAsync();
        }
    }
}
