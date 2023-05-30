using AutoMapper;
using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CategoriaProfile : Profile
{
	public CategoriaProfile()
	{
		CreateMap<CreateCategoriaDto, Categoria>();
		CreateMap<UpdateCategoriaDto, Categoria>();
		CreateMap<Categoria, ReadCategoriaDto>();
		CreateMap<Categoria, UpdateCategoriaDto>();
	}
}
