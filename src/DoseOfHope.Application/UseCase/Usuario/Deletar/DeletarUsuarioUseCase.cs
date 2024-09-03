using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.Usuario;
using DoseOfHope.Exception;
using DoseOfHope.Exception.ExceptionBase;

namespace DoseOfHope.Application.UseCase.Usuario.Deletar;

public class DeletarUsuarioUseCase : IDeletarUsuarioUseCase
{
    private readonly IUsuariosWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarUsuarioUseCase(IUsuariosWriteOnlyRepository usuariosWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _repository = usuariosWriteOnlyRepository;
        _unitOfWork = unitOfWork;   
    }
    public async Task Execute(int codigo)
    {
        var result = await _repository.Delete(codigo);

        if (!result)
            throw new NotFoundException(ResourceErrorMessages.USUARIO_NAO_ENCONTRADO);

        await _unitOfWork.commit();
    }
}
