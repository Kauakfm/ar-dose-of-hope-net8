using DoseOfHope.Communication.Requests;
using DoseOfHope.Exception;
using FluentValidation;

namespace DoseOfHope.Application.UseCase.Usuario.Atualizar;

public class AtualizarValidacaoUsuario : AbstractValidator<RequestUsuarioUpdateJson>
{
    public AtualizarValidacaoUsuario()
    {
        RuleFor(u => u.nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_OBRIGATORIO);
        RuleFor(u => u.email).NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_OBRIGATORIO);
        RuleFor(u => u.dataNascimento).NotEmpty().WithMessage(ResourceErrorMessages.DATA_NASCIMENTO_OBRIGATORIO);
    }
}
