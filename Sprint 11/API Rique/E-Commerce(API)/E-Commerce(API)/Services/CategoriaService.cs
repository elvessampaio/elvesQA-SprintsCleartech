using AutoMapper;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace E_Commerce_API_.Services;

public class CategoriaService : ICategoriaService
{
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private IMapper _mapper;
    private ICategoriaDAO _iCategoriaDAO;
    private ISubcategoriaService _iSubcategoriaService;
    private readonly ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly IProdutoDAO _iProdutoDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;

    public CategoriaService(IMapper mapper, ICategoriaDAO categoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, IProdutoDAO iProdutoDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO, ISubcategoriaService iSubcategoriaService)
    {
        _mapper = mapper;
        _iCategoriaDAO = categoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
        _iSubcategoriaService = iSubcategoriaService;
    }

    public async Task<List<ReadCategoriaDto>> ListaFiltrada(FiltroCategoriaDto filtro)
    {
        var categorias = await _iCategoriaDAO.GetListaCategorias(filtro);

        var categoriasDto = new List<ReadCategoriaDto>();

        _mapper.Map(categorias, categoriasDto);

        return categoriasDto;
    }

    public Result<ReadCategoriaDto> AdicionarCategoria(CreateCategoriaDto categoriaDto)
    {
        if (NomeRepetido(categoriaDto.Nome))
            return Result.Fail("O nome cadastrado já existe.");

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        _iCategoriaDAO.Add(categoria);

        var readCategoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return Result.Ok(readCategoriaDto);
    }

    public Result AtualizarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        var categoria = BuscarCategoriaPorId(id);
        if (categoria == null) return Result.Fail("Categoria não existe.");

        var edicaoValida = EdicaoValida(categoriaDto, categoria);
        if (edicaoValida.IsFailed) return Result.Fail(edicaoValida.Reasons[0].Message);

        _iSubcategoriaService.AlterarStatusSubcategorias(categoriaDto, categoria);

        _mapper.Map(categoriaDto, categoria);

        _iCategoriaDAO.Update(categoria);
        return Result.Ok();
    }

    public Result RemoverCategoria(Guid id)
    {
        var categoria = BuscarCategoriaPorId(id);
        if (categoria == null) return Result.Fail("Categoria não existe.");

        _iCategoriaDAO.Delete(categoria);
        return Result.Ok();
    }

    private Result EdicaoValida(UpdateCategoriaDto categoriaDto, Categoria categoria)
    {
        if(categoria.Nome == categoriaDto.Nome && categoria.Status == categoriaDto.Status)
            return Result.Fail("A edição não foi realizada, pois os valores não foram alterados.");

        if (categoriaDto.Status == false && categoria.Subcategorias.Any(s => s.Produtos.Count > 0))
            return Result.Fail("A categoria não pode ser inativada, pois existem produtos cadastrados em suas subcategorias.");

        if (NomeRepetido(categoriaDto.Nome, categoria.Id))
            return Result.Fail("O nome cadastrado já existe.");

        return Result.Ok();
    }

    private Categoria? BuscarCategoriaPorId(Guid id)
    {
        return _iCategoriaDAO.GetCategorias().FirstOrDefault(c => c.Id == id);
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome && c.Id != id);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }
}