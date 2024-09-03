using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.Usuario.Registrar
{
    public interface IRegistrarUsuarioUseCase
    {
        Task<ResponseRegistrarUsuarioJson> Executar(RequestUsuarioJson request);
    }
}
