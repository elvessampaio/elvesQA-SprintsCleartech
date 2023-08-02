using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Models;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{    
    private ICategoriaService _service;
    
    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {

        var categoria = new Categoria();

        try
        {
            categoria = await _service.CadastrarCategoria(categoriaDto);
        }

        catch(NameExceptions e)
        {
            return BadRequest(e.Message);
        }

         return CreatedAtAction(nameof(PesquisarCategoriaId),
            new { id = categoria.Id }, categoriaDto); 

    }

    [HttpGet]
    public IActionResult PesquisarCategoria([FromQuery] FilterCategoriaDto filtro)
    {
        var categoria = _service.PesquisarCategoria(filtro);
        if (categoria == null) NotFound();
        return Ok(categoria);
    }

    [HttpGet("{id}")] 
    public IActionResult PesquisarCategoriaId(int id)
    {
        var categoria = _service.PesquisarCategoriaId(id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
                       
    }
      
    [HttpPut("{id}")]
    public async Task<IActionResult> EditarCategoria (int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        var categoria = new Categoria();
        if (categoria == null) return NotFound();
        try
        {
            categoria = await _service.EditarCategoria(id, categoriaDto);
            
        }

        catch (NameExceptions e)

        {
            return BadRequest(e.Message);
        }

        return NoContent(); 
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> ApagarCategoria (int id)
    {
        var categoria = await _service.ApagarCategoria(id);
        if (categoria == null) return NotFound();
        return NoContent();
    }

}
