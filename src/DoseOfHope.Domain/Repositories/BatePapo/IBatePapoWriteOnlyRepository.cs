using DoseOfHope.Domain.Entities;

namespace DoseOfHope.Domain.Repositories.BatePapo;

public interface IBatePapoWriteOnlyRepository
{
    Task AddMensagens(tabMensagens mensagens);
    Task AddConversas(tabConversas conversa);
    Task AssociateConversationWithMessage(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo, int conversaCodigo);
    Task<int> CreateNewConversation();

}
