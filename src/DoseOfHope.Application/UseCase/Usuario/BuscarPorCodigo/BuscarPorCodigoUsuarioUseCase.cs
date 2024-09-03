using AutoMapper;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Exception;
using DoseOfHope.Exception.ExceptionBase;
using DoseOfHope.Infrastructure.Encryption.Criptografar;

namespace DoseOfHope.Application.UseCase.Usuario.BuscarPorCodigo;

public class BuscarPorCodigoUsuarioUseCase : IBuscarPorCodigoUsuarioUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICriptografarUseCase _crypt;
    public BuscarPorCodigoUsuarioUseCase(IUsuariosReadOnlyRepository repository, IMapper mapper, ICriptografarUseCase criptografarUseCase)
    {
        _repository = repository;
        _mapper = mapper;
        _crypt = criptografarUseCase;
    }

    public async Task<ResponseUsuarioJson> Execute(int codigo)
    {
        var response = await _repository.GetByCodigo(codigo);

        if (response is null)
        {
            throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);
        }

        var mappedResponse = _mapper.Map<ResponseUsuarioJson>(response);
        mappedResponse.codigo = _crypt.Encrypt(response.codigo.ToString());

        return mappedResponse;
    }

    public async Task<ResponseFotoUsuarioJson> BuscarAvatar(int codigo)
    {
        var response = await _repository.GetByCodigo(codigo);

        if (response is null)
        {
            throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);
        }

        return _mapper.Map<ResponseFotoUsuarioJson>(response);

    }

}
