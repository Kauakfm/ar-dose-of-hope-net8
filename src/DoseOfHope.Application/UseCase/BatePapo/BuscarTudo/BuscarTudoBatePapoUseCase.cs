using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using DoseOfHope.Domain.Repositories;
using DoseOfHope.Domain.Repositories.BatePapo;
using DoseOfHope.Domain.Repositories.Produto;
using DoseOfHope.Domain.Repositories.Usuario;

namespace DoseOfHope.Application.UseCase.BatePapo.BuscarTudo;

public class BuscarTudoBatePapoUseCase : IBuscarTudoBatePapoUseCase
{
    private readonly IUsuariosReadOnlyRepository _repositoryUser;
    private readonly IProdutoReadOnlyRepository _repositoryProduct;
    private readonly IBatePapoReadOnlyRepository _repositoryBatePapo;
    private readonly IBatePapoWriteOnlyRepository _repositoryWriteOnlyBatePapo;
    private readonly IUnitOfWork _unitOfWork;
    public BuscarTudoBatePapoUseCase(IUsuariosReadOnlyRepository repositoryUser,
        IProdutoReadOnlyRepository repositoryProduct,
        IBatePapoReadOnlyRepository repositoryBatePapo,
        IBatePapoWriteOnlyRepository repositoryWriteOnlyBatePapo,
        IUnitOfWork unitOfWork)
    {
        _repositoryUser = repositoryUser;
        _repositoryProduct = repositoryProduct;
        _repositoryBatePapo = repositoryBatePapo;
        _repositoryWriteOnlyBatePapo = repositoryWriteOnlyBatePapo;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ResponseBatePapoJson>> BuscarTodosBatePapos()
    {
        var users = await _repositoryUser.GetUsersWithDonationsAndUserTypeRoleUser();

        var donations = await _repositoryProduct.GetUsersWhatDonated(users.Select(user => user.codigo).ToList());

        var batePapo = users.Select(user => new ResponseBatePapoJson
        {
            codigo = user.codigo,
            nome = user.nome,
            doacao = donations.Where(d => d.usuarioCodigo == user.codigo).ToList(),
            qtdItensDoados = donations.Count(d => d.usuarioCodigo == user.codigo),
            avatar = user.foto!
        }).ToList();

        return batePapo;
    }

    public async Task<ResponseMensagensComCodigoConversaJson> BuscarConversa_BuscarMensagensConversa(int remetenteUsuarioCodigo, int destinatarioUsuarioCodigo)
    {
        var objMen = new tabMensagens();
        objMen = await _repositoryBatePapo.GetByCodigoConversation(remetenteUsuarioCodigo, destinatarioUsuarioCodigo);

        if (objMen is null)
        {
            objMen = new tabMensagens();
            objMen.conversaCodigo = await _repositoryWriteOnlyBatePapo.CreateNewConversation();
            await _repositoryWriteOnlyBatePapo.AssociateConversationWithMessage(remetenteUsuarioCodigo, destinatarioUsuarioCodigo, objMen.conversaCodigo);
        }

        var lstConversationWithNames = await _repositoryBatePapo.GetAllMessagesWithCodeConvesation(objMen.conversaCodigo);
        var objCompleteConversationWithName = new List<ResponseConversasComNomesJson>();

        foreach (var item in lstConversationWithNames)
        {
            var senderName = await _repositoryUser.GetByCodigo(item.RementeUsuarioCodigo);
            var recipientName = await _repositoryUser.GetByCodigo(item.DestinatarioUsuarioCodigo);
            
            objCompleteConversationWithName.Add(new ResponseConversasComNomesJson
            {
                UsuarioCodigo = item.RementeUsuarioCodigo,
                RemetenteNome = senderName!.nome,
                DestinatarioNome = recipientName!.nome!,
                Mensagem = item.Conteudo,
                horaMinuto = item.dataEnvio.ToString("HH:mm")
            });
        }

        return new ResponseMensagensComCodigoConversaJson()
        {
            codigoConversa = objMen.conversaCodigo,
            lstConversas = objCompleteConversationWithName!,
        };
    }

}
