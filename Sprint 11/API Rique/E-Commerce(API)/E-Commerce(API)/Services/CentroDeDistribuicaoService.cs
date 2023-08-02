using AutoMapper;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using FluentResults;
using System.Text.Json;

namespace E_Commerce_API_.Services;

public class CentroDeDistribuicaoService : ICentroDeDistribuicaoService
{
    private ICentroDeDistribuicaoDAO _iCentroDeDistribuicaoDAO;
    private ICategoriaDAO _iCategoriaDAO;
    private ISubcategoriaDAO _iSubcategoriaDAO;
    private IProdutoDAO _iProdutoDAO;
    private IMapper _mapper;

    public CentroDeDistribuicaoService(ICentroDeDistribuicaoDAO iCentroDeDistribuicaoDAO, IMapper mapper, ICategoriaDAO iCategoriaDAO, ISubcategoriaDAO iSubcategoriaDAO, IProdutoDAO iProdutoDAO)
    {
        _iCentroDeDistribuicaoDAO = iCentroDeDistribuicaoDAO;
        _mapper = mapper;
        _iCategoriaDAO = iCategoriaDAO;
        _iSubcategoriaDAO = iSubcategoriaDAO;
        _iProdutoDAO = iProdutoDAO;
    }

    public async Task<Result<ReadCentroDeDistribuicaoDto>> AdicionarCD(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        if (NomeRepetido(centroDeDistribuicaoDto.Nome)) return Result.Fail("O nome cadastrado já existe.");

        var endereco = await EnderecoPorCep(centroDeDistribuicaoDto.CEP);
        if (endereco.Erro == true) return Result.Fail("O CEP informado não existe.");
        if (endereco.Logradouro.Length == 0) return Result.Fail("O CEP informado é geral.");

        var centro = MapeamentosDeCriacao(centroDeDistribuicaoDto, endereco);

        if (EnderecoExiste(centro)) return Result.Fail("O Endereço cadastrado já existe");

        _iCentroDeDistribuicaoDAO.Add(centro);

        var centroDto = _mapper.Map<ReadCentroDeDistribuicaoDto>(centro);

        return Result.Ok(centroDto);
    }

    private CentroDeDistribuicao MapeamentosDeCriacao(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto, Endereco endereco)
    {
        _mapper.Map(endereco, centroDeDistribuicaoDto);
        return _mapper.Map<CentroDeDistribuicao>(centroDeDistribuicaoDto);
    }

    public async Task<Result> EditarCD(Guid id, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto)
    {
        var centroDeDistribuicao = BuscarCDPorId(id);
        if (centroDeDistribuicao == null) return Result.Fail("Centro de distribuição não existe.");

        if (NomeRepetido(centroDeDistribuicaoDto.Nome, centroDeDistribuicao.Id))
            return Result.Fail("O nome cadastrado já existe.");

        var endereco = await EnderecoPorCep(centroDeDistribuicaoDto.CEP);
        if (endereco.Erro == true) return Result.Fail("O CEP informado não existe.");
        if (endereco.Logradouro.Length == 0) return Result.Fail("O CEP informado é geral.");

        if (centroDeDistribuicaoDto.Status == false && centroDeDistribuicao.Produtos.Count() > 0) return Result.Fail("Não é possível inativar um CD que possui produtos vinculados.");

        MapeamentosDeAtualizacao(centroDeDistribuicao, centroDeDistribuicaoDto, endereco);

        if (EnderecoExiste(centroDeDistribuicao)) return Result.Fail("O Endereço cadastrado já existe.");

        _iCentroDeDistribuicaoDAO.Update(centroDeDistribuicao);
        return Result.Ok();
    }

    private void MapeamentosDeAtualizacao(CentroDeDistribuicao centroDeDistribuicao, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto, Endereco endereco)
    {
        _mapper.Map(endereco, centroDeDistribuicaoDto);
        _mapper.Map(centroDeDistribuicaoDto, centroDeDistribuicao);
    }

    public async Task<List<ReadCentroDeDistribuicaoDto>> ListaFiltrada(FiltroCentroDeDistribuicaoDto filtro)
    {
        var centros = await _iCentroDeDistribuicaoDAO.GetCentrosDeDistribuicao(filtro);

        var centrosDto = _mapper.Map<List<ReadCentroDeDistribuicaoDto>>(centros);

        return centrosDto;
    }

    public Result DeletarCD(Guid id)
    {
        var centroDeDistribuicao = BuscarCDPorId(id);
        if (centroDeDistribuicao == null) return Result.Fail("Centro de distribuição não existe.");

        _iCentroDeDistribuicaoDAO.Delete(centroDeDistribuicao);
        return Result.Ok();
    }

    public CentroDeDistribuicao? BuscarCDPorId(Guid id)
    {
        return _iCentroDeDistribuicaoDAO.GetCentros().FirstOrDefault(centros => centros.Id == id);
    }

    private bool NomeRepetido(string nome, Guid? id = null)
    {
        var existeCategoria = _iCategoriaDAO.GetCategorias().Any(c => c.Nome == nome);

        var existeSubcategoria = _iSubcategoriaDAO.GetSubcategorias().Any(s => s.Nome == nome);

        var existeProduto = _iProdutoDAO.GetProdutos().Any(p => p.Nome == nome);

        var existeCD = _iCentroDeDistribuicaoDAO.GetCentros().Any(cd => cd.Nome == nome && cd.Id != id);

        return existeCategoria || existeSubcategoria || existeProduto || existeCD;
    }

    private async Task<Endereco> EnderecoPorCep(string cep)
    {
        using (HttpClient client = new HttpClient())
        {
            var resposta = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            return JsonSerializer.Deserialize<Endereco>(resposta)!;
        }
    }

    private bool EnderecoExiste(CentroDeDistribuicao centro)
    {
        return _iCentroDeDistribuicaoDAO.GetCentros().Any(centros => centros.CEP == centro.CEP 
        && centros.Numero == centro.Numero && centros.Complemento == centro.Complemento && centros.Id != centro.Id);
    }
}
