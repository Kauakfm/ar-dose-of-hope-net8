using DoseOfHope.Communication.Requests;
using DoseOfHope.Exception;
using FluentValidation;

namespace DoseOfHope.Application.UseCase.Usuario;

public class UsuarioValidacao : AbstractValidator<RequestUsuarioJson>
{
    public UsuarioValidacao()
    {
        RuleFor(u => u.nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_OBRIGATORIO);
        RuleFor(u => u.email).NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_OBRIGATORIO);
        RuleFor(u => u.senha).NotEmpty().WithMessage(ResourceErrorMessages.SENHA_OBRIGATORIO);
        RuleFor(u => u.dataNascimento).NotEmpty().WithMessage(ResourceErrorMessages.DATA_NASCIMENTO_OBRIGATORIO);
        //RuleFor(u => u.telefone).NotEmpty().WithMessage(ResourceErrorMessages.TELEFONE_OBRIGATORIO).Length(11).WithMessage(ResourceErrorMessages.TELEFONE_DEVE_CONTER_11_DIGITOS);
        //RuleFor(u => u.generoCodigo).IsInEnum().WithMessage(ResourceErrorMessages.GENERO_OBRIGATORIO);
        //RuleFor(u => u.rg).NotEmpty().WithMessage(ResourceErrorMessages.RG_OBRIGATORIO).MinimumLength(4).WithMessage(ResourceErrorMessages.RG_DEVE_CONTER_5_DIGITOS);
        //RuleFor(u => u.rua).NotEmpty().WithMessage(ResourceErrorMessages.RUA_OBRIGATORIO);
        //RuleFor(u => u.bairro).NotEmpty().WithMessage(ResourceErrorMessages.BAIRRO_OBRIGATORIO);
        //RuleFor(u => u.cidade).NotEmpty().WithMessage(ResourceErrorMessages.CIDADE_OBRIGATORIO);
        //RuleFor(u => u.numeroResidencia).NotEmpty().WithMessage(ResourceErrorMessages.NUMERO_OBRIGATORIO);
        //RuleFor(u => u.complemento).NotEmpty().WithMessage(ResourceErrorMessages.COMPLEMENTO_OBRIGATORIO);
    }
}
