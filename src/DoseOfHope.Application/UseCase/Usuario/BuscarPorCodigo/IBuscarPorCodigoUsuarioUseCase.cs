using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.Usuario.BuscarPorCodigo
{
    public interface IBuscarPorCodigoUsuarioUseCase
    {
        Task<ResponseUsuarioJson> Execute(int codigo);
        Task<ResponseFotoUsuarioJson> BuscarAvatar(int codigo);
    }
}
