using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.BatePapo;

public interface IBatePapoReadOnlyRepository
{
    Task<tabMensagens?> GetByCodigoConversation(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo);
    Task<List<tabMensagens>> GetAllMessagesWithCodeConvesation(int conversationCode);
}
