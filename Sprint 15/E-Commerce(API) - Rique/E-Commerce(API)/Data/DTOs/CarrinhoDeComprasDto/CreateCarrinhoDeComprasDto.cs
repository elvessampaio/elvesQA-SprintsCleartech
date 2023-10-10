using E_Commerce_API_.Attributes;
using E_Commerce_API_.Exceptions;

namespace E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

public class CreateCarrinhoDeComprasDto
{
    [Obrigatorio]
    public Guid ProdutoId { get; set; }
    [Obrigatorio]
    public uint Quantidade { get; set; }
    [Obrigatorio]
    [TamanhoMaximo(8)]
    [TamanhoMinimo(8)]
    public string CEP { get; set; }
    [Obrigatorio]
    public uint Numero { get; set; }
    [Obrigatorio]
    public string Complemento { get; set; }
}
