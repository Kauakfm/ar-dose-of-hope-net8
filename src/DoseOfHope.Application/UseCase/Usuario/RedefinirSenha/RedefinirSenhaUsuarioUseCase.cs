using AutoMapper;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Exception;
using DoseOfHope.Exception.ExceptionBase;
using DoseOfHope.Infrastructure.Email.Templates;
using DoseOfHope.Infrastructure.Encryption.Descriptografar;

namespace DoseOfHope.Application.UseCase.Usuario.RedefinirSenha;

public class RedefinirSenhaUsuarioUseCase : IRedefinirSenhaUsuarioUseCase
{
    private readonly IDescriptografarUseCase _descriptografarUseCase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuariosReadOnlyRepository _repositoryGet;
    private readonly IMapper _mapper;
    private readonly IUsuarioUpdateOnlyRepository _repositoryUpdate;
    private readonly EmailRedefinirSenha _emailRedefinirSenha;

    public RedefinirSenhaUsuarioUseCase(IDescriptografarUseCase descriptografarUseCase,
        IUnitOfWork unitOfWork,
        IUsuariosReadOnlyRepository usuariosReadOnly,
        IMapper mapper,
        IUsuarioUpdateOnlyRepository usuarioUpdateOnly,
        EmailRedefinirSenha emailRedefinirSenha
        )
    {
        _descriptografarUseCase = descriptografarUseCase;
        _unitOfWork = unitOfWork;
        _repositoryGet = usuariosReadOnly;
        _mapper = mapper;
        _repositoryUpdate = usuarioUpdateOnly;
        _emailRedefinirSenha = emailRedefinirSenha;
    }
    public async Task RedefinirSenha(string token, RequestRedefinirSenhaJson request)
    {
        var codigo = _descriptografarUseCase.DescriptografarID(token);

        var response = await _repositoryUpdate.GetByCodigo(codigo) ?? throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);

        _mapper.Map(request, response);
        _repositoryUpdate.Update(response);

        await _unitOfWork.commit();
    }

    public async Task EnviarEmailDeRedefinirSenha(RequestEmailJson request)
    {
        var objUser = await _repositoryGet.GetByEmail(request.Email) ?? throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);

        await _emailRedefinirSenha.EnviaEmail(objUser);
    }

}
