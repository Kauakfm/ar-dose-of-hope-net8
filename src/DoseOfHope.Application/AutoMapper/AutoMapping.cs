using AutoMapper;
using DoseOfHope.Communication.Requests;
using DoseOfHope.Communication.Responses;
using DoseOfHope.Domain.Entities;
using System.ComponentModel;

namespace DoseOfHope.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestUsuarioJson, tabUsuario>()
        .ForMember(dest => dest.foto, opt => opt.MapFrom(src => "https://api.dicebear.com/8.x/bottts-neutral/svg?seed=Max"))
        .ForMember(dest => dest.tipoUsuarioCodigo, opt => opt.MapFrom(src => src.tipoUsuarioCodigo ?? default(int)))
        .ForMember(dest => dest.TipoUsuarioCodigo, opt => opt.Ignore());

        CreateMap<RequestRedefinirSenhaJson,tabUsuario>();
        CreateMap<RequestUsuarioUpdateJson, tabUsuario>();
        CreateMap<RequestUsuarioAvatarJson, tabUsuario>();
    }   

    private void EntityToResponse()
    {
        CreateMap<tabUsuario, ResponseRegistrarUsuarioJson>();
        CreateMap<tabUsuario, ResponseShortUsuarioJson>().ForMember(dest => dest.tipoUsuarioCodigo, opt => opt.MapFrom(src => src.TipoUsuarioCodigo.codigo));
        CreateMap<tabUsuario, ResponseUsuarioJson>();
        CreateMap<tabUsuario, ResponseFotoUsuarioJson>();

        CreateMap<tabProdutoDoado, ResponseProdutosJson>();
        CreateMap<tabProdutoDoado, ResponseShortProdutoJson>()
          .ForMember(dest => dest.nomeUsuario, opt => opt.MapFrom(src => src.Usuario.nome))
          .ForMember(dest => dest.tipoProdutoDescricao, opt => opt.MapFrom(src => src.TipoProduto.descricao))
          .ForMember(dest => dest.statusCodigo, opt => opt.MapFrom(src => src.codigoStatus));
    }

}
