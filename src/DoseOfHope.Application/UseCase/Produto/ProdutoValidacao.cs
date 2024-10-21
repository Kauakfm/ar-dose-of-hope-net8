using DoseOfHope.Communication.Requests;
using FluentValidation;

namespace DoseOfHope.Application.UseCase.Produto;

public class ProdutoValidacao : AbstractValidator<RequestFormularioProdutoJson>
{
    public ProdutoValidacao()
    {
        RuleFor(produto => produto.tipoItem).IsInEnum().WithMessage("Tipo item inválido");
        RuleFor(produto => produto.nomeDoItem).NotEmpty().WithMessage("Preencha o campo nome do item");
        RuleFor(produto => produto.formaFarmaceutica).IsInEnum().WithMessage("Forma farmaceutica inválida");
        RuleFor(produto => produto.tipoCondicao).IsInEnum().WithMessage("Tipo condicao inválido");
        RuleFor(produto => produto.Dosagem).NotEmpty().WithMessage("Preencha o campo de dosagem");
        RuleFor(produto => produto.quantidade).NotEmpty().WithMessage("Preencha o campo de quantidade");
        RuleFor(produto => produto.dataValidade).NotEmpty().WithMessage("Preencha o campo de data validade");
        RuleFor(produto => produto.necessidadeArmazenamento).IsInEnum().WithMessage("Tipo de armazenamento inválido");
        RuleFor(produto => produto.descricaoDetalhada).NotEmpty().WithMessage("Preencha o campo de descrição detalhada");
        RuleFor(x => x.fotos).NotNull().WithMessage("É necessário enviar pelo menos uma imagem.")
            .Must(fotos => fotos != null && fotos.Count > 0)
                .WithMessage("Envie pelo menos uma imagem.");
    }
}
