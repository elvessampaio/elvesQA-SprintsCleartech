using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository;

public class SubcategoriaRepository : ISubcategoriaRepository
{
    private EcommerceContext _context;
    private IMapper _mapper;

    public SubcategoriaRepository(EcommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private SubCategoria BuscarPorId(int id)
    {
        return _context.SubCategorias.FirstOrDefault(sub => sub.Id == id);
    }
    public async Task<SubCategoria> CadastrarSubcategoria(CreateSubCategoriaDto subcategoriaDto)
    {
        SubCategoria subcategoria = _mapper.Map<SubCategoria>(subcategoriaDto);
        await _context.SubCategorias.AddAsync(subcategoria);
        await _context.SaveChangesAsync();
        return subcategoria;
    }
      
    public List<SubCategoria> PesquisarSubcategoria(FilterSubCategoriaDto filtro)
    {
        
        var sql = "SELECT * FROM `ECOMMERCEAPI`.SUBCATEGORIAS WHERE 1=1";

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

        if (filtro.Skip.HasValue && filtro.Take.HasValue)
        {
            sql += $" LIMIT {filtro.Skip.Value}, {filtro.Take.Value}";
        }

        var subcategoria = _context.SubCategorias.FromSqlRaw(sql).ToList();

        return subcategoria;
    }

    public SubCategoria PesquisarSubcategoriaId(int id)
    {
        var subcategoria = BuscarPorId(id);
        return subcategoria;
    }

    public async Task<SubCategoria> EditarSubcategoria(UpdateSubCategoriaDto subcategoriaDto, int id)
    {
        var subcategoria = BuscarPorId(id);
        _mapper.Map(subcategoriaDto, subcategoria);
        await _context.SaveChangesAsync();
        return subcategoria;
    }

    public async Task<SubCategoria> ApagarSubcategoria(int id)
    {
        var subcategoria = BuscarPorId(id);
        _context.Remove(subcategoria);
        await _context.SaveChangesAsync();
        return subcategoria;
    }

}
