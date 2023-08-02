using AutoMapper;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_.Data.DAO;

public class SubcategoriaDAO : ISubcategoriaDAO
{
    private ECommerceContext _context;
    private IMapper _mapper;

    public SubcategoriaDAO(ECommerceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Create(Subcategoria s)
    {
        _context.Subcategorias.Add(s);
        _context.SaveChanges();
    }
    public void Update(UpdateSubcategoriaDto sDto, Subcategoria s)
    {
        _mapper.Map(sDto, s);
        _context.SaveChanges();
    }

    public void Delete(Subcategoria s)
    {
        _context.Remove(s);
        _context.SaveChanges();
    }

    public List<Subcategoria> GetSubcategorias()
    {
        return _context.Subcategorias.ToList();
    }

    public List<ReadSubcategoriaDto> GetListaSubcategorias(FiltroSubcategoriaDto filtro)
    {
        string querie = "SELECT * FROM `E-COMMERCE(API)`.SUBCATEGORIAS WHERE ";

        if (filtro.Nome != null)
        {
            querie += $"LOCATE ('{filtro.Nome}', NOME) AND ";
        }

        switch (filtro.Status)
        {
            case Status.Inativo:
                querie += $"STATUS = FALSE ";
                break;

            case Status.Ativo:
                querie += $"STATUS = TRUE ";
                break;

            case Status.Todos:
                querie += "1=1 ";
                break;
        }

        if (filtro.Ordem == Ordem.Crescente)
            querie += "ORDER BY NOME ";
        else if (filtro.Ordem == Ordem.Decrescente)
            querie += "ORDER BY NOME DESC ";

        querie += $"LIMIT {(filtro.Pagina - 1) * filtro.Registros}, {filtro.Registros}";

        var subcategorias = _context.Subcategorias.FromSqlRaw(querie).ToList();

        var subcategoriasDto = _mapper.Map<List<ReadSubcategoriaDto>>(subcategorias);

        return subcategoriasDto;
    }
}
