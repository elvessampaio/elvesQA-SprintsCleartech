using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class CarrinhoDeComprasController : ControllerBase
{
    private readonly ICarrinhoDeComprasService _service;

    public CarrinhoDeComprasController(ICarrinhoDeComprasService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CriarCarrinho([FromBody] CreateCarrinhoDeComprasDto createDto)
    {
        var carrinho = await _service.CriarCarrinho(createDto);

        return CreatedAtAction(nameof(GetCarrinho), new { carrinhoDeComprasId = carrinho.Id }, carrinho);
    }

    [HttpPost("{carrinhoDeComprasId}")]
    public IActionResult AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, [FromBody] AddProdutosAoCarrinhoDto addDto)
    {
        var carrinho = _service.AdicionarProdutosAoCarrinho(carrinhoDeComprasId, addDto);
        return Ok(carrinho);
    }

    [HttpPut("{carrinhoDeComprasId}")]
    public IActionResult AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId,
                                                               [FromBody] UpdateCarrinhoDeComprasDto updateDto)
    {
        var carrinho = _service.AlterarQuantidadeDeProdutosNoCarrinho(carrinhoDeComprasId, updateDto);
        return Ok(carrinho);
    }

    [HttpGet("{carrinhoDeComprasId}")]
    public IActionResult GetCarrinho(Guid carrinhoDeComprasId)
    {
        var carrinho = _service.GetCarrinho(carrinhoDeComprasId);
        return Ok(carrinho);
    }
}
