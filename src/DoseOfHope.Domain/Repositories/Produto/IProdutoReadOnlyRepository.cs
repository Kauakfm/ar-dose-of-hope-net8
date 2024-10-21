using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.Produto;

public interface IProdutoReadOnlyRepository
{
    Task<List<tabProdutoDoado>> GetAll();
    Task<tabProdutoDoado?> GetByCodigo(int codigo);
    Task<List<tabProdutoDoado>> GetProductsWithUserAndTypeProduct();
    Task<List<tabProdutoDoado>> GetUsersWhatDonated(List<int> userCodes);
    Task<List<tabProdutoDoado>> GetDonationsPutUserCode(int usuarioCodigo);
    Task<List<tabProdutoDoadoImagem>> GetListCodeImagesProduct(int productCode);
}
