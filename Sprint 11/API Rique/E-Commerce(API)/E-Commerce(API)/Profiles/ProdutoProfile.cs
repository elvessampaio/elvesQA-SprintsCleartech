using AutoMapper;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class ProdutoProfile : Profile
{
	public ProdutoProfile()
	{
        CreateMap<CreateProdutoDto, Produto>();
        CreateMap<Produto, ReadProdutoDto>()
            .ForMember(r => r.Subcategoria, opt => opt
            .MapFrom(p => p.Subcategoria.Nome))
            .ForMember(r => r.Categoria, opt => opt
            .MapFrom(p => p.Subcategoria.Categoria.Nome))
            .ForMember(r => r.CentroDeDistribuicao, opt => opt
            .MapFrom(p => p.CentroDeDistribuicao.Nome))
            .ForMember(r => r.Criacao, opt => opt
            .MapFrom(p => p.DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(p => p.Modificacao, opt => opt
            .MapFrom(r => r.DataEHoraModificacao == default ? "Não houve modificações." 
            : r.DataEHoraModificacao.ToString("dd/MM/yyyy HH:mm:ss")));
        CreateMap<UpdateProdutoDto, Produto>();
    }
}
