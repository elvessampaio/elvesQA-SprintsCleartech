using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Models;

namespace Ecommerce_API.Controllers;

[ApiController]
[Route("[controller]")]
public class CentroDistribuicaoController : ControllerBase
{
    private EcommerceContext _context;
    private ICentroDistribuicaoService _service;

    public CentroDistribuicaoController(EcommerceContext context, ICentroDistribuicaoService service)
    {
        _context = context;
        _service = service;
    }

    [HttpPost]

    public async Task<IActionResult> CadastrarCentroDistribuicao([FromBody] CreateCentroDistribuicaoDto centroDto)
    {
        var centro = new CentroDistribuicao();
        try
        {
            centro = await _service.CadastrarCentroDistribuicao(centroDto);

        }

        catch (NameExceptions e)
        {
            return BadRequest(e.Message);
        }

        return CreatedAtAction(nameof(PesquisarCentroDistribuicaoId),
                 new { id = centro.Id }, centroDto);
    }

    [HttpGet]

    public IActionResult PesquisarCentroDistribuicao([FromBody] FilterCentroDistribuicaoDto filtro)
    {
        var centro = _service.PesquisarCentroDistribuicao(filtro);
        if (centro == null) return NotFound();
        return Ok(centro);
    }

    [HttpGet("{id}")]
    public IActionResult PesquisarCentroDistribuicaoId(int id)
    {
        var centro = _service.PesquisarCentroDistribuicaoId(id);
        if (centro == null) return NotFound();

        return Ok(centro);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> EditarCentroDistribuicao([FromBody] UpdateCentroDistribuicaoDto centroDto, int id)
    {
        try
        {
            var centro = await _service.EditarCentroDistribuicao(centroDto, id);
            if (centro == null) return NotFound();
        }

        catch (NameExceptions e)
        {
            return BadRequest(e.Message);
        }

        return Ok(centroDto);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> ApagarCentroDistribuicao(int id)
    {
        var centro = await _service.ApagarCentroDistribuicao(id);
        if (centro == null) return NotFound();
        return NoContent();

    }
}
