using AutoMapper;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Exception;
using DoseOfHope.Exception.ExceptionBase;

namespace DoseOfHope.Application.UseCase.Usuario.Atualizar;

public class AtualizarUsuarioUseCase : IAtualizarUsuarioUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioUpdateOnlyRepository _repository;

    public AtualizarUsuarioUseCase(IMapper mapper, IUnitOfWork unitOfWork, IUsuarioUpdateOnlyRepository usuarioUpdateOnlyRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;   
        _repository = usuarioUpdateOnlyRepository;
    }

    public async Task Execute(int codigo, RequestUsuarioUpdateJson request)
    {
        Validar(request);

        var usuario = await _repository.GetByCodigo(codigo);

        if(usuario is null)
            throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);

        _mapper.Map(request, usuario);

        _repository.Update(usuario);

        await _unitOfWork.commit();
    }

    private void Validar(RequestUsuarioUpdateJson request)
    {
        var response = new AtualizarValidacaoUsuario().Validate(request);

        if (!response.IsValid)
        {
            var errorMessages = response.Errors.Select(errorMessages => errorMessages.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

    public async Task AtualizarAvatarUsuario(int codigo, RequestUsuarioAvatarJson request)
    {
        var usuario = await _repository.GetByCodigo(codigo);

        if(usuario is null)
            throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);

        _mapper.Map(request, usuario);

        _repository.Update(usuario);    

        await _unitOfWork.commit(); 
    }
}
