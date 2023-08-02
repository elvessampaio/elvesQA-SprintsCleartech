using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]

public class CentroDeDistribuicaoController : ControllerBase
{
    private ICentroDeDistribuicaoService _iCentroDeDistribuicaoService;

    public CentroDeDistribuicaoController(ICentroDeDistribuicaoService iCentroDeDistribuicaoService)
    {
        _iCentroDeDistribuicaoService = iCentroDeDistribuicaoService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarCD([FromBody] CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        var cadastro = await _iCentroDeDistribuicaoService.AdicionarCD(centroDeDistribuicaoDto);

        return cadastro.IsSuccess 
            ? CreatedAtAction(nameof(PesquisarCD), new { nome = cadastro.Value.Nome }, cadastro.Value) 
            : BadRequest(cadastro.Reasons);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarCD(Guid id, [FromBody] UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        var update = await _iCentroDeDistribuicaoService.EditarCD(id, centroDeDistribuicaoDto);

        return update.IsSuccess ? NoContent() : BadRequest(update.Reasons);
    }

    [HttpGet]
    public async Task<IActionResult> PesquisarCD([FromBody] FiltroCentroDeDistribuicaoDto filtro)
    {
        var centros = await _iCentroDeDistribuicaoService.ListaFiltrada(filtro);

        return centros.Any() ? Ok(centros) : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult RemoverCD(Guid id)
    {
        var remover = _iCentroDeDistribuicaoService.DeletarCD(id);

        return remover.IsSuccess ? NoContent() : NotFound(remover.Reasons);
    }
}
