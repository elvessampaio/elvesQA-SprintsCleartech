using Gerardor_de_Cupons_API__.Data;
using Gerardor_de_Cupons_API__.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gerardor_de_Cupons_API__.Controllers;
[Route("[controller]")]
[ApiController]
public class GeradorDeCuponsController : ControllerBase
{
    private IGeradorDeCuponsService _service;

	public GeradorDeCuponsController(IGeradorDeCuponsService service)
	{
		_service = service;
	}

	[HttpPost]
	public IActionResult GerarCupomDeDesconto(CreateCupom createCupom)
	{
		var cupom = _service.GerarCupomDeDesconto(createCupom);

		return Ok(cupom);
	}
}
