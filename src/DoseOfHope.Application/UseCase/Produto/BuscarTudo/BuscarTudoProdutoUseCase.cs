using AutoMapper;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Repositories.Produto;

namespace DoseOfHope.Application.UseCase.Produto.BuscarTudo;

public class BuscarTudoProdutoUseCase : IBuscarTudoProdutoUseCase
{
    private readonly IProdutoReadOnlyRepository _produtoReadOnlyRepository;
    private readonly IMapper _mapper;

    public BuscarTudoProdutoUseCase(IProdutoReadOnlyRepository produtoReadOnlyRepository, IMapper mapper)
    {
        _produtoReadOnlyRepository = produtoReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseProdutosJson> GetProductsWithUserAndTypeProduct()
    {
        var result = await _produtoReadOnlyRepository.GetProductsWithUserAndTypeProduct();

        return new ResponseProdutosJson
        {
            Produtos = _mapper.Map<List<ResponseShortProdutoJson>>(result)
        };
    }
}
