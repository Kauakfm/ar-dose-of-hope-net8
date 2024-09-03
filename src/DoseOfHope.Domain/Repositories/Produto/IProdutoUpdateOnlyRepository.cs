using DoseOfHope.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoseOfHope.Domain.Repositories.Produto
{
    public interface IProdutoUpdateOnlyRepository
    {
        Task<tabProdutoDoado?> GetByCodigo(int codigo);
        void Update(tabProdutoDoado produto);

    }
}
