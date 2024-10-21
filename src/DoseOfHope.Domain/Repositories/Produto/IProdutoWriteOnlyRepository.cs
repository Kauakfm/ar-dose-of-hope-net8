using DoseOfHope.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Domain.Repositories.Produto
{
    public interface IProdutoWriteOnlyRepository
    {
        Task Add(tabProdutoDoado produto);
        Task AddImageProduct(tabProdutoDoadoImagem produto);

        Task<bool> Delete(int codigo);
    }
}
