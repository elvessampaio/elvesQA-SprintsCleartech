using AutoMapper;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_API.Services.Interfaces;
using Ecommerce_API.Repository.Interfaces;

namespace Ecommerce_API.Services;

public class CategoriaService : ICategoriaService
{
    private EcommerceContext _context;
    private IMapper _mapper;
    private ICategoriaRepository _repository;

    public CategoriaService(EcommerceContext context, IMapper mapper, ICategoriaRepository repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    private List<Categoria> ListaCategoria()
    {
        return _context.Categorias.ToList();
    }

   
    public async Task<Categoria> CadastrarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);                      
        VerificacaoDosDados(categoriaDto.Nome);               
        await _repository.CadastrarCategoria(categoriaDto);
        return categoria;

    }

    public List<ReadCategoriaDto> PesquisarCategoria(FilterCategoriaDto filtro)
    {
        var categoria = _repository.PesquisarCategoria(filtro);
        if (categoria == null) return null;

        var categoriaDto = new List<ReadCategoriaDto>();
        _mapper.Map(categoria, categoriaDto);
      
        return categoriaDto;

    }
      
    public ReadCategoriaDto PesquisarCategoriaId(int id)
    {
        var categoria = _repository.PesquisarCategoriaId(id);
        if (categoria == null) return null;
        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return categoriaDto;

    }

    public async Task<Categoria> EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
        if (categoria == null) return null;
        VerificacaoDosDados(categoriaDto.Nome, id);
        AlteracaoStatus(id, categoriaDto);
        await _repository.EditarCategoria(categoriaDto, id);
        

        return categoria;
    }

    public async Task<Categoria> ApagarCategoria(int id)
    {
        var categoria = await _repository.ApagarCategoria(id);
        if (categoria == null) return null;
        return categoria;
    }

   
    private void AlteracaoStatus(int id, UpdateCategoriaDto categoriaDto)
    {
        if (categoriaDto.Status == false)
        {

            foreach (var sub in _context.SubCategorias)
            {
                if (sub.CategoriaId == id)
                {
                    sub.Status = false;
                    sub.DataModificacao = DateTime.Now;
                }

            }

            foreach (var prod in _context.Produtos)
            {
                if (prod.CategoriaId == id)
                {
                    prod.Status = false;
                    prod.DataModificacao = DateTime.Now;
                }
            }

        }

        if (categoriaDto.Status == true)
        {
            foreach (var sub in _context.SubCategorias)
            {
                if (sub.CategoriaId == id)
                {
                    sub.Status = true;
                    sub.DataModificacao = DateTime.Now;
                }
            }

            foreach (var prod in _context.Produtos)
            {
                if (prod.CategoriaId == id)
                {
                    prod.Status = true;
                    prod.DataModificacao = DateTime.Now;
                }
            }
        }
    }

   
    private void VerificacaoDosDados(string nome, int? id = null)
    {
        if (ListaCategoria().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
        {
            throw new NameExceptions("Já existe uma Categoria com o mesmo nome.");
        }

        if (_context.SubCategorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe uma SubCategoria com o mesmo nome.");
        }

        if (_context.Produtos.Any(p => p.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe um Produto com o mesmo nome.");
        }

        if (_context.CentrosDistribuicoes.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe um Centro de Distribuição com o mesmo nome.");
        }
    }
   
}
