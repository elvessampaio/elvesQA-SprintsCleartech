using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoriaController : ControllerBase
{
    private ISubcategoriaService _service;
    public SubCategoriaController(ISubcategoriaService service)
    {
        _service = service;
    }

    [HttpPost]

    public async Task<IActionResult> CadastrarSubCategoria([FromBody] CreateSubCategoriaDto subCategoriaDto)
    {
        var subCategoria = new SubCategoria();

        try
        {
            subCategoria = await _service.CadastrarSubCategoria(subCategoriaDto);
        }

        catch(NameExceptions e)
        {
            return BadRequest(e.Message);

        }

        return CreatedAtAction(nameof(PesquisarSubCategoriaId),
            new { id = subCategoria.Id }, subCategoriaDto);

    }

    [HttpGet]
    public IActionResult PesquisarSubCategoria([FromQuery] FilterSubCategoriaDto filtro)
    {
        var subCategoria = _service.PesquisarSubcategoria(filtro);
        if (subCategoria == null) return NotFound();
        return Ok(subCategoria);

    }


    [HttpGet("{id}")]

    public IActionResult PesquisarSubCategoriaId(int id)
    {
        var subCategoria = _service.PesquisarSubCategoriaId(id);
        if (subCategoria == null) return NotFound();
        return Ok(subCategoria);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> EditarSubCategoria([FromBody] UpdateSubCategoriaDto subCategoriaDto, int id)
    {
        var subCategoria = new SubCategoria();
        if (subCategoria == null) return NotFound();
        try
        {
            subCategoria = await _service.EditarSubCategoria(subCategoriaDto, id);
            
        }

        catch(NameExceptions e)
        {
            return BadRequest(e.Message);  
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task <IActionResult> ApagarSubCategoria(int id)
    {
        var subCategoria = await _service.ApagarSubCategoria(id);
        if (subCategoria == null) return NotFound();
        return NoContent();

    }

}
