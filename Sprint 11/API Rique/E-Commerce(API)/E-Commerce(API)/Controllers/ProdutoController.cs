using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Interfaces;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class ProdutoController : ControllerBase
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private IMapper _mapper;
    private IProdutoService _iProdutoService;
    private ISubcategoriaService _iSubcategoriaService;
    public ProdutoController(IMapper mapper, ISubcategoriaService iSubcategoriaService, IProdutoService iProdutoService)
    {
        _mapper = mapper;
        _iSubcategoriaService = iSubcategoriaService;
        _iProdutoService = iProdutoService;
    }

    [HttpPost]
    public IActionResult CadastrarProduto([FromBody] CreateProdutoDto produtoDto)
    {
        var cadastrar = _iProdutoService.AdicionarProduto(produtoDto);

        return cadastrar.IsSuccess 
            ? CreatedAtAction(nameof(PesquisarProduto), new { nome = cadastrar.Value.Nome }, cadastrar.Value) 
            : BadRequest(cadastrar.Reasons);
    }

    [HttpGet]
    public IActionResult PesquisarProduto([FromQuery] FiltroProdutoDto filtroDto)
    {
        var produtos = _iProdutoService.ListaProdutos(filtroDto);

        return produtos.Any()
            ? Ok(produtos) 
            : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult EditarProduto(Guid id, [FromBody] UpdateProdutoDto produtoDto)
    {
        var update = _iProdutoService.AtualizarProduto(id, produtoDto);

        return update.IsSuccess ? NoContent() : BadRequest(update.Reasons);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverProduto(Guid id)
    {
        var remover = _iProdutoService.RemoverProduto(id);

        return remover.IsSuccess ? NoContent() : NotFound();
    }
}
