using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Models;
using Ecommerce_API.Data.DTOS.SubCategoria;

namespace Ecommerce_API.Repository.Interfaces;

public interface ISubcategoriaRepository
{
    public Task<SubCategoria> CadastrarSubcategoria(CreateSubCategoriaDto subcategoriaDto);
    public SubCategoria PesquisarSubcategoriaId(int id);
    public List<SubCategoria> PesquisarSubcategoria(FilterSubCategoriaDto filtro);
    public Task<SubCategoria> EditarSubcategoria(UpdateSubCategoriaDto subcategoriaDto, int id);
    public Task<SubCategoria> ApagarSubcategoria(int id);
}
