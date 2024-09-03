using AutoMapper;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Repositories.Usuario;

namespace DoseOfHope.Application.UseCase.Usuario.BucarTudo;

public class BuscarTudoUsuarioUseCase : IBuscarTudoUsuarioUseCase
{
    private readonly IUsuariosReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public BuscarTudoUsuarioUseCase(IUsuariosReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseUsuariosJson> Execute()
    {
        var result = await _repository.GetAllUsersAndTypeUser();

        return new ResponseUsuariosJson
        {
            Usuarios = _mapper.Map<List<ResponseShortUsuarioJson>>(result)
        };
    }
}

