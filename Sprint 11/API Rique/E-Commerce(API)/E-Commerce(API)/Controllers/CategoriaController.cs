using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private ICategoriaService _iCategoriaService;

    public CategoriaController(ICategoriaService iCategoriaService)
    {
        _iCategoriaService = iCategoriaService;
    }

    [HttpPost]
    public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        var cadastro = _iCategoriaService.AdicionarCategoria(categoriaDto);

        return cadastro.IsSuccess 
            ? CreatedAtAction(nameof(PesquisarCategorias), new { nome = cadastro.Value.Nome }, cadastro.Value) 
            : BadRequest(cadastro.Reasons);
    }

    [HttpGet]
    public async Task<IActionResult> PesquisarCategorias([FromQuery] FiltroCategoriaDto filtro) 
    {
        var categorias = await _iCategoriaService.ListaFiltrada(filtro);

        return categorias.Any() ? Ok(categorias) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult EditarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        var edicao = _iCategoriaService.AtualizarCategoria(id, categoriaDto);

        return edicao.IsSuccess ? NoContent() : BadRequest(edicao.Reasons);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverCategoria(Guid id)
    {
        var remover = _iCategoriaService.RemoverCategoria(id);

        return remover.IsSuccess ? NoContent() : NotFound(remover.Reasons);
    }
}