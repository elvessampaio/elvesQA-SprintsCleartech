using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private EcommerceContext _context;
        private IProdutoService _service;

        public ProdutoController(EcommerceContext context, IProdutoService service)
        {
            _context = context;
            _service = service;
        }
                
        [HttpPost]
        public async Task<IActionResult> CadastrarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            var produto = new Produto();

            try
            {
                produto = await _service.CadastrarProduto(produtoDto);
            }

            catch (NameExceptions e)
            {
                return BadRequest(e.Message);
            }
                               
            return CreatedAtAction(nameof(PesquisarProdutoId),
                new { id = produto.Id }, produtoDto);

        }

        [HttpGet]

        public IActionResult PesquisarProduto([FromQuery] FilterProdutoDto filtro)

        {
            var produto = _service.PesquisarProduto(filtro);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpGet("{id}")]

        public IActionResult PesquisarProdutoId(int id)
        {
            var produto = _service.PesquisarProdutoId(id);
            if (produto == null) return NotFound();
            return Ok(produto);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> EditarProduto([FromBody] UpdateProdutoDto produtoDto, int id)
        {
            try
            {
                var produto = await _service.EditarProduto(produtoDto, id);
                if (produto == null) return NotFound();
            }
            catch (NameExceptions e)
            {
                return BadRequest(e.Message);
            }
           
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> ApagarProduto(int id)
        {
            var produto = await _service.ApagarProduto(id);
            if (produto == null) return NotFound();
            return NoContent();

        }

    }
}