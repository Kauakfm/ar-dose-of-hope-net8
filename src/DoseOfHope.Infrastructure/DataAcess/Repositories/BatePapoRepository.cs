using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.BatePapo;
using Microsoft.EntityFrameworkCore;

namespace DoseOfHope.Infrastructure.DataAcess.Repositories;

internal class BatePapoRepository : IBatePapoReadOnlyRepository, IBatePapoWriteOnlyRepository
{
    private readonly DoseOfHopeDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    public BatePapoRepository(DoseOfHopeDbContext doseOfHopeDbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = doseOfHopeDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task AddMensagens(tabMensagens mensagens)
    {
        await _dbContext.tabMensagens.AddAsync(mensagens);
    }
    public async Task AddConversas(tabConversas conversa)
    {
        await _dbContext.tabConversas.AddAsync(conversa);
    }

    public async Task<tabMensagens?> GetByCodigoConversation(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo)
    {
        var messages = await _dbContext.tabMensagens.FirstOrDefaultAsync(men =>
        (men.RementeUsuarioCodigo == remetenteUsuarioCodigo && men.DestinatarioUsuarioCodigo == destinatarioUsuarioCodigo) ||
        (men.RementeUsuarioCodigo == remetenteUsuarioCodigo && men.DestinatarioUsuarioCodigo == destinatarioUsuarioCodigo));

        if (messages == null)
            return null;

        return messages;
    }

    public async Task AssociateConversationWithMessage(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo, int conversaCodigo)
    {
        await _dbContext.tabMensagens.AddAsync(new tabMensagens
        {
            conversaCodigo = conversaCodigo,
            RementeUsuarioCodigo = remetenteUsuarioCodigo,
            DestinatarioUsuarioCodigo = destinatarioUsuarioCodigo,
            Conteudo = "Olá! Tudo bem?",
            dataEnvio = DateTime.UtcNow
        });

        await _unitOfWork.commit();
    }

    public async Task<int> CreateNewConversation()
    {
        var conversation = new tabConversas();
        await _dbContext.tabConversas.AddAsync(conversation);
        await _unitOfWork.commit();

        return conversation.codigo;
    }

    public async Task<List<tabMensagens>> GetAllMessagesWithCodeConvesation(int conversationCode) =>
        await _dbContext.tabMensagens.Where(x => x.conversaCodigo == conversationCode).OrderBy(y => y.dataEnvio).ToListAsync();
}
