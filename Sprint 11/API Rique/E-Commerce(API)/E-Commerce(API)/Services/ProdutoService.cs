using AutoMapper;
using E_Commerce_API_.Models;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Interfaces;
using FluentResults;

namespace E_Commerce_API_.Services;

public class ProdutoService : IProdutoService
{
    private IMapper _mapper;
    private IProdutoDAO _iProdutoDAO;
    private readonly ICategoriaDAO _iCategoriaDAO;
    private readonly ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;


    public ProdutoService(IMapper mapper, IProdutoDAO iProdutoDao, ICategoriaDAO iCategoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO)
    {
        _mapper = mapper;
        _iProdutoDAO = iProdutoDao;
        _iCategoriaDAO = iCategoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
    }

    public List<ReadProdutoDto> ListaProdutos(FiltroProdutoDto filtro)
    {
        return _iProdutoDAO.GetListaProdutos(filtro);
    }

    public Produto? BuscarProdutoPorId(Guid id)
    {
        return _iProdutoDAO.GetProdutos().FirstOrDefault(p => p.Id == id);
    }

    public Result<ReadProdutoDto> AdicionarProduto(CreateProdutoDto produtoDto)
    {
        var subcategoria = _iSubcategoriaDAO.GetSubcategorias().FirstOrDefault(s => s.Id == produtoDto.SubcategoriaId);
        if (subcategoria == null) return Result.Fail("A subcategoria não existe.");

        var cadastroValido = CadastroValido(produtoDto, subcategoria);
        if (cadastroValido.IsFailed) return Result.Fail(cadastroValido.Reasons[0].Message);

        var produto = _mapper.Map<Produto>(produtoDto);

        _iProdutoDAO.Add(produto);

        return Result.Ok(_mapper.Map<ReadProdutoDto>(produto));
    }

    private Result CadastroValido(CreateProdutoDto produtoDto, Subcategoria subcategoria)
    {
        if (subcategoria.Status == false)
            return Result.Fail($"A subcategoria {subcategoria.Nome} está inativa e não pode receber novos cadastros de produtos.");

        if (NomeRepetido(produtoDto.Nome))
            return Result.Fail("O nome cadastrado já existe.");

        if (!ValoresValidos(produtoDto))
            return Result.Fail("Valores informados para cadastro de produto não são válidos.");

        return Result.Ok();
    }

    public Result AtualizarProduto(Guid id, UpdateProdutoDto produtoDto)
    {
        var produto = BuscarProdutoPorId(id);
        if (produto == null) return Result.Fail("O produto não existe.");

        var edicaoValida = EdicaoValida(produtoDto, produto);
        if (edicaoValida.IsFailed) return Result.Fail(edicaoValida.Reasons[0].Message);

        _iProdutoDAO.Update(produtoDto, produto);

        return Result.Ok();
    }

    private Result EdicaoValida(UpdateProdutoDto produtoDto, Produto produto)
    {
        if (!ValoresAlterados(produtoDto, produto))
            return Result.Fail("A edição não foi feita, pois os valores não foram alterados.");

        if (NomeRepetido(produtoDto.Nome, produto.Id))
            return Result.Fail("O nome cadastrado já existe.");

        return Result.Ok();
    }

    public Result RemoverProduto(Guid id)
    {
        var produto = BuscarProdutoPorId(id);
        if (produto == null) return Result.Fail("O produto não existe.");

        _iProdutoDAO.Delete(produto);
        return Result.Ok();
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome && p.Id != id);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }

    public bool ValoresValidos(CreateProdutoDto produtoDto)
    {
        if (produtoDto.Peso <= 0) return false;

        if (produtoDto.Altura <= 0) return false;

        if (produtoDto.Largura <= 0) return false;

        if (produtoDto.Comprimento <= 0) return false;

        if (produtoDto.Valor <= 0) return false;

        if (produtoDto.QuantidadeEmEstoque <= 0) return false;

        return true;
    }

    public bool ValoresAlterados(UpdateProdutoDto produtoDto, Produto produto)
    {
        return !(produtoDto.Nome == produto.Nome &&
        produtoDto.Descricao == produto.Descricao &&
        produtoDto.Peso == produto.Peso &&
        produtoDto.Altura == produto.Altura &&
        produtoDto.Largura == produto.Largura &&
        produtoDto.Comprimento == produto.Comprimento &&
        produtoDto.Valor == produto.Valor &&
        produtoDto.QuantidadeEmEstoque == produto.QuantidadeEmEstoque &&
        produtoDto.Status == produto.Status);
    }
}
