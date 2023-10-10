using AutoMapper;
using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Profiles;

public class CarrinhoDeComprasProfile : Profile
{
	public CarrinhoDeComprasProfile()
	{
		CreateMap<Endereco, CarrinhoDeCompras>();
		CreateMap<CreateCarrinhoDeComprasDto, CarrinhoDeCompras>();
		CreateMap<CarrinhoDeCompras, ReadCarrinhoDeComprasDto>()
			.ForMember(readCarrinho => readCarrinho.ValorTotal, opt => opt
			.MapFrom(carrinho => carrinho.ValorTotal.ToString("C")))
			.ForMember(readCarrinho => readCarrinho.Produtos, opt => opt
			.Ignore());
		CreateMap<UpdateCarrinhoDeComprasDto, ProdutoNoCarrinho>()
			.ForMember(p => p.ProdutoId, opt => opt
			.Ignore());
	}
}
