using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Controllers;

//Anotações do Controller
[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase //A classe herda o ControllerBase
{
    //O controlador vai utilizar o context que será responsável por acessar o banco de dados
    /*AutoMapper será um mapeamento automático de um DTO para uma categoria.
    Como são duas classes que possuem as mesmas propriedades, o AutoMapper conseguirá fazer isso de maneira implícita
    sempre que precisar mapear parâmetro por parâmetro.*/

    private CategoriaContext _context;
    private IMapper _mapper; 

    //construtor
    public CategoriaController(CategoriaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //o IActionResult é um retorno de uma possível ação usados no Http
    //O post é para cadastrar a informação
    [HttpPost]
    public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        if (_context.Categorias.Any(c => c.Nome.ToUpper() == categoriaDto.Nome.ToUpper())) //O Any é usado para procurar qualquer objeto na lista usando a lambda
        {
            return BadRequest("Já existe uma Categoria com o mesmo nome");
        }
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto); //vai transformar uma CategoriaDTO em uma categoria
        _context.Categorias.Add(categoria);
        _context.SaveChanges(); //para salvar as informações
        return CreatedAtAction(nameof(PesquisarCategoriaId),
            new { id = categoria.Id }, categoria);
    }

    [HttpGet("PesquisaTodos")]

    public ActionResult<Categoria> PesquisarCategoriaTodos()
    {
        var categoria = _context.Categorias.ToList();
        if (categoria == null) return NotFound();

        return Ok(categoria);

    }



    [HttpGet("PesquisaFiltro")]

    public ActionResult<IEnumerable<Categoria>> PesquisarCategoriaFiltro(
        [FromQuery][RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
        [MinLength(3, ErrorMessage = "O campo deve não pode ter menos que 3 caracteres")]
        [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")] 
        string? nome, 
        [FromQuery] bool decrescente, 
        [FromQuery] bool? status)
    {
        var categoria = _context.Categorias.AsEnumerable(); //o AsEnumerable é um método para chamar um DataTable

        if (!string.IsNullOrEmpty(nome))
        {
            categoria = categoria.Where(c => c.Nome.Contains(nome.ToUpper()));
        }


        if (status.HasValue)
        {
            categoria = categoria.Where(c => c.Status == status.Value);
        }

        //else
        //{
        //    categoria = categoria.Where(c => c.Status == false);
        //}

      
        if (decrescente == true)
        {
            categoria = categoria.OrderByDescending(c => c.Nome);
        }

        else
        {
            categoria = categoria.OrderBy(c => c.Nome);
        }


        var categorias = categoria.ToList();

        return categorias;

       
    }
    

    [HttpGet("{id}")] //o get recebe o id como parâmetro para fazer a pesquisa direcionada

    public ActionResult<Categoria> PesquisarCategoriaId(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        
        return Ok(categoria);
        
    }

    //o put é para atualizar
    [HttpPut("{id}")]
    public IActionResult EditarCategoria (int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
       
        var categoria = _context.Categorias.FirstOrDefault(categoria =>categoria.Id == id); //para localizar a categoria cadastrada
        if (categoria == null) return NotFound();

        if (_context.Categorias.Any(c => c.Nome == categoriaDto.Nome)) //O Any é usado para procurar qualquer objeto na lista usando a lambda
        {
            return BadRequest("Já existe uma Categoria com o mesmo nome");
        }

        _mapper.Map(categoriaDto, categoria); //fazer o mapeamento da minha categoriaDto para a categoria
                
        _context.SaveChanges();
        
        return NoContent(); //para que faça a atualização e retorne o statuscode (204).
    }

    [HttpDelete("{id}")]

    public IActionResult ApagarCategoria (int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id); //para localizar a categoria cadastrada
        if (categoria == null) return NotFound();
        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }

}
