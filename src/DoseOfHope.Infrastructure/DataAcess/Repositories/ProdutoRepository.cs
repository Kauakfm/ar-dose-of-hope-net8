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
        public async Task AddImageProduct(tabProdutoDoadoImagem produto)
        {
            await _dbContext.tabProdutoDoadoImagem.AddAsync(produto);
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

        public async Task<List<tabProdutoDoado>> GetDonationsPutUserCode(int usuarioCodigo)
        {
            var result = await _dbContext.tabProdutoDoado.Where(prod => prod.usuarioCodigo == usuarioCodigo)
                .Include(status => status.statusCodigo)
                .Include(tipoProduto => tipoProduto.TipoProduto)
                .Include(formaFarmaceutica => formaFarmaceutica.FormaFarmaceutica)
                .Include(tipoCondicao => tipoCondicao.TipoCondicao)
                .Include(tipoNecessidadeArmazenamentoCodigo => tipoNecessidadeArmazenamentoCodigo.TipoNecessidadeArmazenamento)
                .ToListAsync();

            return result.Select(obj =>
            {
                obj.validadeEscrita = DateTime.TryParse(obj.validadeEscrita, out DateTime validade) ? validade.ToString("MM/yyyy") : obj.validadeEscrita;
                return obj;
            }).ToList();
        }

        public async Task<List<tabProdutoDoadoImagem>> GetListCodeImagesProduct(int productCode)
        {
            return await _dbContext.tabProdutoDoadoImagem.Where(image => image.produtoCodigo == productCode).ToListAsync();
        }
    }
}
