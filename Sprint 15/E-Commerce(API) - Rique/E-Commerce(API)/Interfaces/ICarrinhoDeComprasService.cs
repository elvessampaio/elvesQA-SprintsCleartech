using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ICarrinhoDeComprasService
{
    ReadCarrinhoDeComprasDto AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, AddProdutosAoCarrinhoDto addDto);
    ReadCarrinhoDeComprasDto AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId, UpdateCarrinhoDeComprasDto updateDto);
    Task<ReadCarrinhoDeComprasDto> CriarCarrinho(CreateCarrinhoDeComprasDto create);
    ReadCarrinhoDeComprasDto GetCarrinho(Guid carrinhoDeComprasId);
}
