using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.BatePapo;
using DoseOfHope.Domain.Repositories.Usuario;
using Microsoft.AspNetCore.SignalR;

namespace DoseOfHope.Application.UseCase.BatePapo.SignalR;

public class SignalRBatePapoUseCase : Hub
{
    private readonly IBatePapoWriteOnlyRepository _repositoryAdd;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuariosReadOnlyRepository _repositoryUser;
    public SignalRBatePapoUseCase(IBatePapoWriteOnlyRepository repositoryAdd, IUnitOfWork unitOfWork,
        IUsuariosReadOnlyRepository repositoryUser)
    {
        _repositoryAdd = repositoryAdd;
        _unitOfWork = unitOfWork;
        _repositoryUser = repositoryUser;
    }

    public async Task SendMessage(int conversaCodigo, int remetenteCodigo, int destinatarioCodigo, string conteudo)
    {
        var mensagens = new tabMensagens
        {
            conversaCodigo = conversaCodigo,
            RementeUsuarioCodigo = remetenteCodigo,
            DestinatarioUsuarioCodigo = destinatarioCodigo,
            Conteudo = conteudo,
            dataEnvio = DateTime.Now
        };
        await _repositoryAdd.AddMensagens(mensagens);
        await _unitOfWork.commit();

        var remetente = await _repositoryUser.GetByCodigo(remetenteCodigo);
        var destinatario = await _repositoryUser.GetByCodigo(destinatarioCodigo);

        var objConversa = new ResponseConversaComNomeJson
        {
            UsuarioCodigo = remetenteCodigo,
            RemetenteNome = remetente!.nome,
            DestinatarioNome = destinatario!.nome,
            Mensagem = conteudo,
            horaMinuto = mensagens.dataEnvio.ToString("HH:mm")
        };

        await Clients.Group(conversaCodigo.ToString()).SendAsync("ReceiveMessage", objConversa);
    }
    public async Task JoinConversation(int conversaCodigo)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conversaCodigo.ToString());
    }

}