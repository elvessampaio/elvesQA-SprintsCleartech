using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Interfaces;

public interface IProdutoService
{
    public List<ReadProdutoDto> ListaProdutos(FiltroProdutoDto filtroDto);
    public Produto? BuscarProdutoPorId(Guid id);
    public Result<ReadProdutoDto> AdicionarProduto(CreateProdutoDto produtoDto);
    public Result AtualizarProduto(Guid id, UpdateProdutoDto produtoDto);
    public Result RemoverProduto(Guid id);
}
