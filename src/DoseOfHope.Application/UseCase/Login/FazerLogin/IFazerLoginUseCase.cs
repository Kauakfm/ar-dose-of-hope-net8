using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.Login.FazerLogin;

public interface IFazerLoginUseCase
{
    Task<ResponseRegistrarUsuarioJson> Execute(RequestLoginJson request);
    Task<ResponseRegistrarUsuarioJson> LoginSemSenha(int usuarioCodigo);
}
