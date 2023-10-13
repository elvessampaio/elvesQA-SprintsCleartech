using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Models;

namespace E_Commerce_API_.Interfaces;

public interface ICarrinhoDeComprasService
{
    Task<ReadCarrinhoDeComprasDto> AddEnderecoDeEntregaAoCarrinho(Guid carrinhoDeComprasId, AddEnderecoDeEntregaDto enderecoDto);
    ReadCarrinhoDeComprasDto AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, AddProdutosAoCarrinhoDto addDto);
    ReadCarrinhoDeComprasDto AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId, Guid produtoId, UpdateCarrinhoDeComprasDto updateDto);
    Task<ReadCarrinhoDeComprasDto> CriarCarrinho(CreateCarrinhoDeComprasDto create);
    Task<ReadCarrinhoDeComprasDto> EditarEnderecoDeEntregaDoCarrinho(Guid carrinhoDeComprasId, UpdateEnderecoDeEntregaDto updateDto);
    void ExcluirCarrinho(Guid carrinhoDeComprasId);
    ReadCarrinhoDeComprasDto GetCarrinho(Guid carrinhoDeComprasId);
    ReadCarrinhoDeComprasDto InserirCupomDeDesconto(Guid carrinhoDeComprasId, string cupom);
    ReadCarrinhoDeComprasDto RemoverCupomDeDesconto(Guid carrinhoDeComprasId);
    object RemoverProdutoDoCarrinho(Guid carrinhoDeComprasId, Guid produtoId);
}
