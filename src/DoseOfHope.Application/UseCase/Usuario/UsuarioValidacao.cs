using DoseOfHope.Communication.Requests;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Exception;
using FluentValidation;

namespace DoseOfHope.Application.UseCase.Usuario;

public class UsuarioValidacao : AbstractValidator<RequestUsuarioJson>
{
    private readonly IUsuariosReadOnlyRepository _usuarioRepository;

    public UsuarioValidacao(IUsuariosReadOnlyRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;

        RuleFor(u => u.nome).NotEmpty().WithMessage(ResourceErrorMessages.NOME_OBRIGATORIO);
        RuleFor(u => u.email).NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_OBRIGATORIO)
               .MustAsync(EmailUnico).WithMessage(ResourceErrorMessages.EMAIL_JA_CADASTRADO);
        RuleFor(u => u.senha).NotEmpty().WithMessage(ResourceErrorMessages.SENHA_OBRIGATORIO);
        RuleFor(u => u.dataNascimento).NotEmpty().WithMessage(ResourceErrorMessages.DATA_NASCIMENTO_OBRIGATORIO);
        RuleFor(u => u.cpf).NotEmpty().WithMessage(ResourceErrorMessages.CPF_OBRIGATORIO)
                .MustAsync(CpfUnico).WithMessage(ResourceErrorMessages.CPF_JA_CADASTRADO);
        //RuleFor(u => u.telefone).NotEmpty().WithMessage(ResourceErrorMessages.TELEFONE_OBRIGATORIO).Length(11).WithMessage(ResourceErrorMessages.TELEFONE_DEVE_CONTER_11_DIGITOS);
        //RuleFor(u => u.generoCodigo).IsInEnum().WithMessage(ResourceErrorMessages.GENERO_OBRIGATORIO);
        //RuleFor(u => u.rg).NotEmpty().WithMessage(ResourceErrorMessages.RG_OBRIGATORIO).MinimumLength(4).WithMessage(ResourceErrorMessages.RG_DEVE_CONTER_5_DIGITOS);
        //RuleFor(u => u.rua).NotEmpty().WithMessage(ResourceErrorMessages.RUA_OBRIGATORIO);
        //RuleFor(u => u.bairro).NotEmpty().WithMessage(ResourceErrorMessages.BAIRRO_OBRIGATORIO);
        //RuleFor(u => u.cidade).NotEmpty().WithMessage(ResourceErrorMessages.CIDADE_OBRIGATORIO);
        //RuleFor(u => u.numeroResidencia).NotEmpty().WithMessage(ResourceErrorMessages.NUMERO_OBRIGATORIO);
        //RuleFor(u => u.complemento).NotEmpty().WithMessage(ResourceErrorMessages.COMPLEMENTO_OBRIGATORIO);
    }
    private async Task<bool> EmailUnico(string email, CancellationToken cancellationToken)
    {
        return !await _usuarioRepository.EmailExisteAsync(email);
    }

    private async Task<bool> CpfUnico(string cpf, CancellationToken cancellationToken)
    {
        return !await _usuarioRepository.CpfExisteAsync(cpf);
    }
}
