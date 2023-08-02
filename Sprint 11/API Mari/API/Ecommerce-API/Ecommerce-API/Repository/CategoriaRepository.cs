using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository;

public class CategoriaRepository : ICategoriaRepository
{
    private EcommerceContext _context;
    private IMapper _mapper;

    public CategoriaRepository(EcommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private Categoria BuscarPorId (int id)
    {
        return _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
    }

    public async Task<Categoria> CadastrarCategoria(CreateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public List<Categoria> PesquisarCategoria(FilterCategoriaDto filtro)
    {
       var sql = "SELECT * FROM `ECOMMERCEAPI`.CATEGORIAS WHERE 1=1";

        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            sql += $" AND LOCATE ('{filtro.Nome}', NOME)";
        }

        if (filtro.Status == false)
        {
            sql += $" AND STATUS = FALSE";
        }

        if (filtro.Status == true)
        {
            sql += $" AND STATUS = TRUE";
        }

        if (filtro.Desc == true)
        {
            sql += $" ORDER BY NOME DESC";
        }

        if (filtro.Desc == false)
        {
            sql += $" ORDER BY NOME";
        }

        var categoria = _context.Categorias.FromSqlRaw(sql).ToList();

        return categoria;
    }

    public Categoria PesquisarCategoriaId(int id)
    {
       var categoria = BuscarPorId(id);
        return categoria;
    }

    public async Task<Categoria> EditarCategoria(UpdateCategoriaDto categoriaDto, int id)
    {
        var categoria = BuscarPorId(id);
        _mapper.Map(categoriaDto, categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> ApagarCategoria(int id)
    {
        var categoria = BuscarPorId(id);
        _context.Remove(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }
}
