using DoseOfHope.Communication.Responses;

namespace DoseOfHope.Application.UseCase.BatePapo.BuscarTudo
{
    public interface IBuscarTudoBatePapoUseCase
    {
        Task<List<ResponseBatePapoJson>> BuscarTodosBatePapos();
        Task<ResponseMensagensComCodigoConversaJson> BuscarConversa_BuscarMensagensConversa(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo);
    }
}
