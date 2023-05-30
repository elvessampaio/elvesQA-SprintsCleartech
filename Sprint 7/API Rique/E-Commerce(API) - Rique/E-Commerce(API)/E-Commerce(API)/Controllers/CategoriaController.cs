using AutoMapper;
using AutoMapper.Configuration;
using E_Commerce_API_.Attributes;
using E_Commerce_API_.Data;
using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace E_Commerce_API_.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private CategoriaContext _context;
    private IMapper _mapper;

    public CategoriaController(CategoriaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto); //Converte a categoriaDto para o tipo Categoria

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        categoria.Nome = categoria.Nome.ToLower();
        categoria.Nome = ti.ToTitleCase(categoria.Nome);

        if (_context.Categorias.Any(categoriaExistente => categoriaExistente.Nome == categoria.Nome)) return BadRequest("O nome da categoria já existe.");

        categoria.Id = Guid.NewGuid();
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return CreatedAtAction(nameof(PesquisarCategoriaPorNome), new { nome = categoria.Nome }, categoriaDto);
    }

    [HttpGet]
    public IActionResult PesquisarCategoriaPorNome([VerificacaoDaPesquisa] string? nome, [FromQuery] char ordem = 'c', [FromQuery] char status = 'a') // ordem = c (Crescente) ou d (Decrescente), status = a (Todos) ou t (Ativo) ou f (Inativo)
    {
        string sts = "";
        if (status == 'a' || status == 'A') sts = "Todos";
        else if (status == 't' || status == 'T') sts = "Ativo";
        else if (status == 'f' || status == 'F') sts = "Inativo";

        var categoria = new List<Categoria>();

        if (nome == null)
        {
            categoria = _context.Categorias.ToList();
        }
        else
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            nome = nome.ToLower();
            categoria = _context.Categorias.Where(categoria => categoria.Nome.Contains(nome) || categoria.Nome.Contains(ti.ToTitleCase(nome))).ToList();
            if (categoria.Count == 0) return NotFound();
        }


        var categoriaDto = new List<ReadCategoriaDto>();
        _mapper.Map(categoria, categoriaDto);

        for (int i = 0; i < categoriaDto.Count; i++) //Formata as datas e o status para o tipo String, gerando uma visualização mais amigável.
        {
            categoriaDto[i].Criacao = categoria[i].DataEHoraCriacao.ToString("dd/MM/yyyy HH:mm:ss");
            if (categoria[i].DataEHoraModificacao == new DateTime())
            {
                categoriaDto[i].Modificacao = "Não houve modificações.";
            }
            else
            {
                categoriaDto[i].Modificacao = categoria[i].DataEHoraModificacao.ToString("dd/MM/yyyy HH:mm:ss");
            }
            if (categoria[i].Status == true) categoriaDto[i].Status = "Ativo";
            else categoriaDto[i].Status = "Inativo";
        }

        if (ordem == 'c' && (sts == "Ativo" || sts == "Inativo"))
        {
            return Ok(categoriaDto.Where(categoriaDto => categoriaDto.Status == sts).OrderBy(categoriaDto => categoriaDto.Nome));
        }

        else if (ordem == 'c' && sts == "Todos")
        {
            return Ok(categoriaDto.OrderBy(categoriaDto => categoriaDto.Nome));
        }

        else if (ordem == 'd' && (sts == "Ativo" || sts == "Inativo"))
        {
            return Ok(categoriaDto.Where(categoriaDto => categoriaDto.Status == sts).OrderByDescending(categoriaDto => categoriaDto.Nome));
        }

        else if (ordem == 'd' && sts == "Todos")
        {
            return Ok(categoriaDto.OrderByDescending(categoriaDto => categoriaDto.Nome));
        }

        else return BadRequest();

    }

    [HttpPut]
    public IActionResult EditarCategoria([FromQuery] Guid id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
        categoriaDto.Nome = categoriaDto.Nome.ToLower();
        categoriaDto.Nome = ti.ToTitleCase(categoriaDto.Nome);

        if (_context.Categorias.Any(categoriaExistente => categoriaExistente.Nome == categoriaDto.Nome)) return BadRequest("O nome da categoria já existe.");

        _mapper.Map(categoriaDto, categoria);
        categoria.DataEHoraModificacao = DateTime.Now;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch]
    public IActionResult EditarCategoriaParcial([FromQuery] Guid id, JsonPatchDocument<UpdateCategoriaDto> patch)
    {
        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        var verificarNome = new UpdateCategoriaDto();

        patch.ApplyTo(verificarNome);

        if (verificarNome.Nome != null)
        {
            if (_context.Categorias.Any(categoriaExistente => categoriaExistente.Nome == ti.ToTitleCase(verificarNome.Nome.ToLower()))) return BadRequest("O nome da categoria já existe.");
        }

        var categoriaParaAtualizar = _mapper.Map<UpdateCategoriaDto>(categoria);

        patch.ApplyTo(categoriaParaAtualizar, ModelState);

        if (!TryValidateModel(categoriaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        categoriaParaAtualizar.Nome = categoriaParaAtualizar.Nome.ToLower();
        categoriaParaAtualizar.Nome = ti.ToTitleCase(categoriaParaAtualizar.Nome);

        _mapper.Map(categoriaParaAtualizar, categoria);
        categoria.DataEHoraModificacao = DateTime.Now;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete]
    public IActionResult RemoverCategoria([FromQuery] Guid id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        _context.Remove(categoria);
        _context.SaveChanges();
        return NoContent();
    }
}