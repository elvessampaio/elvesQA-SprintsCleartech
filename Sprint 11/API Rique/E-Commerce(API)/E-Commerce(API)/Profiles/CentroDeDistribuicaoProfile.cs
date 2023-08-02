using AutoMapper;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CentroDeDistribuicaoProfile : Profile
{
	public CentroDeDistribuicaoProfile()
	{
		CreateMap<CreateCentroDeDistribuicaoDto, CentroDeDistribuicao>();
		CreateMap<CentroDeDistribuicao, ReadCentroDeDistribuicaoDto>()
			.ForMember(dto => dto.Criacao, opt => opt
			.MapFrom(c => c.DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss")))
			.ForMember(dto => dto.Modificacao, opt => opt
			.MapFrom(c => c.DataEHoraModificacao == default ? "Não houve modificações." 
			: c.DataEHoraModificacao.ToString("dd/MM/ss HH:mm:ss")));
		CreateMap<UpdateCentroDeDistribuicaoDto, CentroDeDistribuicao>();
		CreateMap<Endereco, CreateCentroDeDistribuicaoDto>();
		CreateMap<Endereco, UpdateCentroDeDistribuicaoDto>();
	}
}
