using AutoMapper;
using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Data.DTOs.SubcategoriaDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Services;

public class SubcategoriaService : ISubcategoriaService
{
    private IMapper _mapper;
    private ISubcategoriaDAO _iSubcategoriaDAO;
    private readonly ICategoriaDAO _iCategoriaDAO;
    private readonly IProdutoDAO _iProdutoDAO;
    private readonly ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;

    public SubcategoriaService(IMapper mapper, ISubcategoriaDAO iSubcategoriaDAO, ICategoriaDAO iCategoriaDAO, IProdutoDAO iProdutoDAO, ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO)
    {
        _mapper = mapper;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iCategoriaDAO = iCategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
    }

    public List<ReadSubcategoriaDto> ListaFiltrada(FiltroSubcategoriaDto filtro)
    {
        return _iSubcategoriaDAO.GetListaSubcategorias(filtro);
    }

    public Result<ReadSubcategoriaDto> AdicionarSubcategoria(CreateSubcategoriaDto subcategoriaDto)
    {
        var categoria = _iCategoriaDAO.GetCategorias().FirstOrDefault(categoria => categoria.Id == subcategoriaDto.CategoriaId);
        if (categoria == null) return Result.Fail("Categoria não existe.");

        var adicaoValida = CadastroValido(subcategoriaDto, categoria);
        if (adicaoValida.IsFailed) return Result.Fail(adicaoValida.Reasons[0].Message);

        var subcategoria = _mapper.Map<Subcategoria>(subcategoriaDto);

        _iSubcategoriaDAO.Create(subcategoria);

        var readSubcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);
        return Result.Ok(readSubcategoriaDto);
    }

    private Result CadastroValido(CreateSubcategoriaDto subcategoriaDto, Categoria categoria)
    {
        if (categoria.Status == false)
            return Result.Fail($"A categoria {categoria.Nome} está inativa e não pode receber cadastros de subcategorias.");

        if (NomeRepetido(subcategoriaDto.Nome))
            return Result.Fail("O nome cadastrado já existe.");

        return Result.Ok();
    }

    public Result AtualizarSubcategoria(Guid id, UpdateSubcategoriaDto subcategoriaDto)
    {
        var subcategoria = BuscarSubcategoriaPorId(id);
        if (subcategoria == null) return Result.Fail("Subcategoria não existe.");

        var edicaoValida = EdicaoValida(subcategoriaDto, subcategoria);
        if (edicaoValida.IsFailed) return Result.Fail(edicaoValida.Reasons[0].Message);

        _iSubcategoriaDAO.Update(subcategoriaDto, subcategoria);
        return Result.Ok();
    }

    private Result EdicaoValida(UpdateSubcategoriaDto subcategoriaDto, Subcategoria subcategoria)
    {
        if (subcategoria.Nome == subcategoriaDto.Nome && subcategoria.Status == subcategoriaDto.Status)
            return Result.Fail("Nenhum valor foi alterado, a edição não foi realizada.");

        if (subcategoria.Categoria.Status == false)
            return Result.Fail($"Enquanto a categoria {subcategoria.Categoria.Nome} está inativa, suas subcategorias não podem ser editadas.");

        if (subcategoriaDto.Status == false && subcategoria.Produtos.Any())
            return Result.Fail("A subcategoria não pode ser inativada, pois existem produtos cadastrados.");

        if (NomeRepetido(subcategoriaDto.Nome, subcategoria.Id))
            return Result.Fail("O nome cadastrado já existe.");

        return Result.Ok();
    }

    public Result RemoverSubcategoria(Guid id)
    {
        var subcategoria = BuscarSubcategoriaPorId(id);
        if (subcategoria == null) return Result.Fail("Subcategoria não existe.");

        _iSubcategoriaDAO.Delete(subcategoria);
        return Result.Ok();
    }

    public Subcategoria? BuscarSubcategoriaPorId(Guid id)
    {
        return _iSubcategoriaDAO.GetSubcategorias().FirstOrDefault(s => s.Id == id);
    }

    public void AlterarStatusSubcategorias(UpdateCategoriaDto categoriaDto, Categoria categoria)
    {
        if (categoriaDto.Status != categoria.Status)
        {
            var subcategorias = categoria.Subcategorias.ToList();
            foreach (var subcategoria in subcategorias)
            {
                subcategoria.Status = categoriaDto.Status;
                subcategoria.DataEHoraModificacao = DateTime.Now;
            }
        }
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome && s.Id != id);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }
}
