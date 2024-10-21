using AutoMapper;
using DoseOfHope.Communication.Enums;
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

        CreateMap<RequestRedefinirSenhaJson, tabUsuario>();
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

        CreateMap<tabProdutoDoado, ResponseShortDoacaoProdutoJson>()
           .ForMember(dest => dest.tipoProdutoDescricao, opt => opt.MapFrom(src => src.TipoProduto.descricao))
           .ForMember(dest => dest.nomeDoItem, opt => opt.MapFrom(src => src.nome))
           .ForMember(dest => dest.formaFarmaceuticaDescricao, opt => opt.MapFrom(src => src.formaFarmaceuticaCodigo.HasValue ? src.FormaFarmaceutica.Descricao : null))
           .ForMember(dest => dest.tipoCondicaoDescricao, opt => opt.MapFrom(src => src.tipoCondicaoCodigo.HasValue ? src.TipoCondicao.Descricao : null))
           .ForMember(dest => dest.dosagemEscrita, opt => opt.MapFrom(src => src.dosagemEscrita))
           .ForMember(dest => dest.quantidade, opt => opt.MapFrom(src => src.qntd.HasValue ? src.qntd.Value : 0))
           .ForMember(dest => dest.validadeEscrita, opt => opt.MapFrom(src => src.validadeEscrita))
           .ForMember(dest => dest.tipoNecessidadeArmazenamentoDescricao, opt => opt.MapFrom(src => src.tipoNecessidadeArmazenamentoCodigo.HasValue ? src.TipoNecessidadeArmazenamento.Descricao : null))
           .ForMember(dest => dest.descricaoDetalhada, opt => opt.MapFrom(src => src.descricaoDetalhada));

    }
}
