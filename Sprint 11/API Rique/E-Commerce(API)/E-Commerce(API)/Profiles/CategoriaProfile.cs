using AutoMapper;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CategoriaProfile : Profile
{
	public CategoriaProfile()
	{
		CreateMap<CreateCategoriaDto, Categoria>();
		CreateMap<UpdateCategoriaDto, Categoria>();
		CreateMap<Categoria, ReadCategoriaDto>()
			.ForMember(categoriaDto => categoriaDto.Criacao, opt => opt
			.MapFrom(categoria => categoria.DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
			.ForMember(categoriaDto => categoriaDto.Modificacao, opt => opt
			.MapFrom(categoria => categoria.DataEHoraModificacao == default ? "Não houve modificações." : categoria.DataEHoraModificacao.ToString("dd/MM/yyyy HH:mm:ss")));
	}
}
