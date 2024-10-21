using DoseOfHope.Application.UseCase.AmazonS3.Registrar;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.Produto;
using DoseOfHope.Exception.ExceptionBase;
using System.ComponentModel.DataAnnotations;

namespace DoseOfHope.Application.UseCase.Produto.Registrar;

public class RegistrarProdutoUseCase : IRegistrarProdutoUseCase
{
    private readonly IRegistrarAmazonS3UseCase _registerS3;
    private readonly IProdutoWriteOnlyRepository _registerProduct;
    private readonly IUnitOfWork _unitOfWork;


    public RegistrarProdutoUseCase(IRegistrarAmazonS3UseCase s3Register,
        IProdutoWriteOnlyRepository registerProduct,
        IUnitOfWork unitOfWork)
    {
        _registerS3 = s3Register;
        _registerProduct = registerProduct;
        _unitOfWork = unitOfWork;
    }


    public async Task<ResponseRegistrarProdutoJson> Executar(RequestFormularioProdutoJson request, int usuarioCodigo)
    {

        Validate(request);

        var objProduto = new tabProdutoDoado
        {
            usuarioCodigo = usuarioCodigo,
            codigoStatus = 1, //pendente
            tipoProdutoCodigo = Convert.ToInt32(request.tipoItem),
            nome = request.nomeDoItem,
            formaFarmaceuticaCodigo = Convert.ToInt32(request.formaFarmaceutica),
            tipoCondicaoCodigo = Convert.ToInt32(request.tipoCondicao),
            dosagemEscrita = request.Dosagem,
            qntd = request.quantidade,
            validadeEscrita = request.dataValidade,
            tipoNecessidadeArmazenamentoCodigo = Convert.ToInt32(request.necessidadeArmazenamento),
            descricaoDetalhada = request.descricaoDetalhada,
        };

        await _registerProduct.Add(objProduto);
        await _unitOfWork.commit();


        var objRegisterS3 = new RequestAmazonS3UploadJson();

        if (request.fotos != null)
        {
            foreach (var item in request.fotos)
            {
                objRegisterS3.diretorio = "fotoProduto";
                objRegisterS3.arquivo = item;

                var response = await _registerS3.SalvarArquivoS3(objRegisterS3);

                var objProdutoImagem = new tabProdutoDoadoImagem
                {
                    produtoCodigo = objProduto.codigo,
                    nomeImagem = response
                };

                await _registerProduct.AddImageProduct(objProdutoImagem);
                await _unitOfWork.commit();
            }
        }

        return new ResponseRegistrarProdutoJson();
    }

    
    private void Validate(RequestFormularioProdutoJson request)
    {
        var validator = new ProdutoValidacao(); 

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var erroMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(erroMessages);
        }

    }
}
