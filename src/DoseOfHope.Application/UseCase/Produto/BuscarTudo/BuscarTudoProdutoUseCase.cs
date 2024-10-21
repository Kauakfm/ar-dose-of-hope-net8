using Amazon.S3;
using AutoMapper;
using DoseOfHope.Application.UseCase.AmazonS3.BuscarImagem;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories.Produto;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace DoseOfHope.Application.UseCase.Produto.BuscarTudo;

public class BuscarTudoProdutoUseCase : IBuscarTudoProdutoUseCase
{
    private readonly IProdutoReadOnlyRepository _produtoReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IBuscarAmazonS3UseCase _amazonS3;

    public BuscarTudoProdutoUseCase(IProdutoReadOnlyRepository produtoReadOnlyRepository,
        IMapper mapper,
        IBuscarAmazonS3UseCase amazonS3)
    {
        _produtoReadOnlyRepository = produtoReadOnlyRepository;
        _mapper = mapper;
        _amazonS3 = amazonS3;
    }

    public async Task<ResponseProdutosJson> GetProductsWithUserAndTypeProduct()
    {
        var result = await _produtoReadOnlyRepository.GetProductsWithUserAndTypeProduct();

        return new ResponseProdutosJson
        {
            Produtos = _mapper.Map<List<ResponseShortProdutoJson>>(result)
        };
    }

    public async Task<ResponseDoacoesProdutosJson> ListarTodasAsDoacoesProdutosDoados(int usuarioCodigo)
    {
        var result = await _produtoReadOnlyRepository.GetDonationsPutUserCode(usuarioCodigo);

        var productsMap = _mapper.Map<List<ResponseShortDoacaoProdutoJson>>(result);

        foreach (var item in productsMap)
        {
            var objImages = await _produtoReadOnlyRepository.GetListCodeImagesProduct(item.codigo);

            item.urlImages = new List<string?>();

            foreach (var image in objImages)
            {
                var url = await _amazonS3.GetUrlFile(new RequestAmazonS3GetJson
                {
                    fileName = image.nomeImagem,
                    directoryName = "fotoProduto"
                });

                item.urlImages.Add(url);
            }
        }
        return new ResponseDoacoesProdutosJson { Produtos = productsMap };
    }


}
