using Ecommerce_API.Data.DTOS;
using Ecommerce_API.Data.DTOS.Categoria;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;

namespace Ecommerce_API.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        public Task<Categoria> CadastrarCategoria(CreateCategoriaDto categoriaDto);
        public Categoria PesquisarCategoriaId(int id);
        public List<Categoria> PesquisarCategoria(FilterCategoriaDto filtro);
        public Task<Categoria> EditarCategoria(UpdateCategoriaDto categoriaDto, int id);
        public Task<Categoria> ApagarCategoria(int id);
    }
}
