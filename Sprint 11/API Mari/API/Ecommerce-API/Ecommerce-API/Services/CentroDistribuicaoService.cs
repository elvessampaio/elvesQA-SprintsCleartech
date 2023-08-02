using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.CentroDistribuicao;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerce_API.Services;

public class CentroDistribuicaoService : ICentroDistribuicaoService
{
    EcommerceContext _context;
    IMapper _mapper;
    ICentroDistribuicaoRepository _repository;

    public CentroDistribuicaoService(EcommerceContext context, IMapper mapper, ICentroDistribuicaoRepository repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Endereco> ConsultarViaCep(string cep)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            var jsonObject = JsonSerializer.Deserialize<Endereco>(response);
            return jsonObject;
        }

        catch
        {
            throw new NameExceptions("O CEP digitado está incorreto.");
        }

    }

    private List<CentroDistribuicao> ListaCentros()
    {
        return _context.CentrosDistribuicoes.ToList();
    }

    public async Task<CentroDistribuicao> CadastrarCentroDistribuicao([FromBody] CreateCentroDistribuicaoDto centroDto)
    {
        var viaCep = await ConsultarViaCep(centroDto.CEP);
        _mapper.Map(viaCep, centroDto);
        var centro = _mapper.Map<CentroDistribuicao>(centroDto);
        VerificacaoDosDados(centroDto.Nome, centroDto.Numero, centroDto.Complemento, centroDto.CEP);
        await _repository.CadastrarCentroDistribuicao(centroDto);
        return centro;
    }

    public List<ReadCentroDistribuicaoDto> PesquisarCentroDistribuicao(FilterCentroDistribuicaoDto filtro)
    {
        var centro = _repository.PesquisarCentroDistribuicao(filtro);
        if (centro == null) return null;
        var centroDto = new List<ReadCentroDistribuicaoDto>();
        _mapper.Map(centro, centroDto);
        return centroDto;
    }

    public ReadCentroDistribuicaoDto PesquisarCentroDistribuicaoId(int id)
    {
        var centro = _repository.PesquisarCentroDistribuicaoId(id);
        var centroDto = _mapper.Map<ReadCentroDistribuicaoDto>(centro);
        return centroDto;
    }
    public async Task<CentroDistribuicao> EditarCentroDistribuicao(UpdateCentroDistribuicaoDto centroDto, int id)
    {
        var viaCep = await ConsultarViaCep(centroDto.CEP);
        _mapper.Map(viaCep, centroDto);
        var centro = _mapper.Map<CentroDistribuicao>(centroDto);
        VerificacaoDosDados(centroDto.Nome, centroDto.Numero, centroDto.Complemento, centroDto.CEP, centroDto.Status, id);
        await _repository.EditarCentroDistribuicao(centroDto, id);
        return centro;
    }

    public async Task<CentroDistribuicao> ApagarCentroDistribuicao(int id)
    {
        var centro = await _repository.ApagarCentroDistribuicao(id);
        return centro;
    }


    private void VerificacaoDosDados(string nome, int numero, string complemento, string cep, bool? status = null, int? id = null)
    {
        if (_context.Categorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe uma Categoria com o mesmo nome.");
        }

        if (_context.SubCategorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe uma SubCategoria com o mesmo nome.");
        }

        if (_context.Produtos.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
        {
            throw new NameExceptions("Já existe um Produto com o mesmo nome.");
        }

        if (ListaCentros().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
        {
            throw new NameExceptions("Já existe um Centro de Distribuição com o mesmo nome.");
        }

        if (ListaCentros().Any(c => c.CEP == cep && c.Numero == numero
        && c.Complemento.ToUpper() == complemento.ToUpper() && c.Id != id))
        {
            throw new NameExceptions("Já existe um Centro de Distribuição com o mesmo endereço.");
        }

        if (_context.Produtos.Where(c => c.CentroDistribuicaoId == id).Count() > 0 && status == false)
        {
            throw new NameExceptions("Há produtos ativos neste Centro de Distribuição, por este motivo, não é possível inativá-lo.");
        }

        if (cep.Contains('-'))
        {
            throw new NameExceptions("Por favor, digite o CEP sem o '-' (hífen).");
        }
    }

}
