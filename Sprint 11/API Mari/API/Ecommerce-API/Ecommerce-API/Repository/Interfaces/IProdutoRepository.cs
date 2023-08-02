using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;

namespace Ecommerce_API.Repository.Interfaces;

public interface IProdutoRepository
{
    public Task<Produto> CadastrarProduto(CreateProdutoDto produtoDto);
    public Produto PesquisarProdutoId(int id);
    public List<Produto> PesquisarProduto(FilterProdutoDto filtro);
    public Task<Produto> EditarProduto(UpdateProdutoDto produtoDto, int id);
    public Task<Produto> ApagarProduto(int id);
}
