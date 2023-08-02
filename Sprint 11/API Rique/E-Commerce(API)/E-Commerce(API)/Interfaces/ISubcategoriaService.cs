using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Interfaces;

public interface ISubcategoriaService
{
    List<ReadSubcategoriaDto> ListaFiltrada(FiltroSubcategoriaDto filtro);
    public Result<ReadSubcategoriaDto> AdicionarSubcategoria(CreateSubcategoriaDto subcategoriaDto);
    public Result AtualizarSubcategoria(Guid id, UpdateSubcategoriaDto subcategoriaDto);
    public Result RemoverSubcategoria(Guid id);
    public Subcategoria? BuscarSubcategoriaPorId(Guid id);
    public void AlterarStatusSubcategorias(UpdateCategoriaDto categoriaDto, Categoria categoria);
}
