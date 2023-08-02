using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class SubcategoriaController : ControllerBase
{
    private ICategoriaService _iCategoriaService;
    private ISubcategoriaService _iSubcategoriaService;

    public SubcategoriaController(ICategoriaService iCategoriaService, ISubcategoriaService iSubcategoriaService)
    {
        _iCategoriaService = iCategoriaService;
        _iSubcategoriaService = iSubcategoriaService;
    }

    [HttpPost]
    public IActionResult CadastrarSubcategoria([FromBody] CreateSubcategoriaDto subcategoriaDto)
    {
        var cadastro = _iSubcategoriaService.AdicionarSubcategoria(subcategoriaDto);

        return cadastro.IsSuccess 
            ? CreatedAtAction(nameof(PesquisarSubcategorias), new { nome = cadastro.Value.Nome }, cadastro.Value) 
            : BadRequest(cadastro.Reasons);
    }

    [HttpGet]
    public IActionResult PesquisarSubcategorias([FromQuery] FiltroSubcategoriaDto filtro)
    {
        var subcategorias = _iSubcategoriaService.ListaFiltrada(filtro);

        return subcategorias.Any() ? Ok(subcategorias) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult EditarSubcategoria(Guid id, [FromBody] UpdateSubcategoriaDto subcategoriaDto)
    {
        var edicao = _iSubcategoriaService.AtualizarSubcategoria(id, subcategoriaDto);

        return edicao.IsSuccess ? NoContent() : BadRequest(edicao.Reasons);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverSubcategoria(Guid id)
    {
        var remover = _iSubcategoriaService.RemoverSubcategoria(id);

        return remover.IsSuccess ? NoContent() : BadRequest(remover.Reasons);
    }
}
