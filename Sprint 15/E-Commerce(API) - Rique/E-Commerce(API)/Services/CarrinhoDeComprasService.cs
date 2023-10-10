using AutoMapper;
using E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;
using E_Commerce_API_.Data.DTOs.ProdutoDto;
using E_Commerce_API_.Exceptions;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using Serilog;
using System.Text.Json;

namespace E_Commerce_API_.Services;

public class CarrinhoDeComprasService : ICarrinhoDeComprasService
{
    private readonly IProdutoService _produtoService;
    private readonly ICarrinhoDeComprasDAO _carrinhoDeComprasDAO;
    private readonly IMapper _mapper;


    public CarrinhoDeComprasService(IProdutoService produtoService, IMapper mapper, ICarrinhoDeComprasDAO carrinhoDeComprasDAO)
    {
        _produtoService = produtoService;
        _mapper = mapper;
        _carrinhoDeComprasDAO = carrinhoDeComprasDAO;
    }

    public async Task<ReadCarrinhoDeComprasDto> CriarCarrinho(CreateCarrinhoDeComprasDto create)
    {
        var produto = _produtoService.PesquisarProdutoId(create.ProdutoId);

        VerificaEstoqueEStatus(create.Quantidade, produto);

        var endereco = await EnderecoPorCep(create.CEP);

        var carrinho = _mapper.Map<CarrinhoDeCompras>(endereco);
        _mapper.Map(create, carrinho);

        var produtoNoCarrinho = new ProdutoNoCarrinho(carrinho.Id, produto.Id, create.Quantidade, produto.Valor);

        _carrinhoDeComprasDAO.Create(carrinho, produtoNoCarrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    private static void VerificaEstoqueEStatus(uint quantidade, ReadProdutoDto produto)
    {
        if (produto.QuantidadeEmEstoque < 1 || produto.QuantidadeEmEstoque < quantidade)
            throw new BadRequestException("Não existe estoque para o produto informado.");

        if (produto.Status == false)
            throw new BadRequestException("O produto informado está inativo e não pode ser adicionado ao carrinho.");
    }

    public ReadCarrinhoDeComprasDto AdicionarProdutosAoCarrinho(Guid carrinhoDeComprasId, AddProdutosAoCarrinhoDto addDto)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        if (carrinho.Produtos.Any(p => p.ProdutoId == addDto.ProdutoId))
            throw new BadRequestException("O Id de produto informado já está no carrinho.");

        var produto = _produtoService.PesquisarProdutoId(addDto.ProdutoId);

        VerificaEstoqueEStatus(addDto.Quantidade, produto);

        var produtoNoCarrinho = new ProdutoNoCarrinho(carrinho.Id, produto.Id, addDto.Quantidade, produto.Valor);

        _carrinhoDeComprasDAO.AddProdutoAoCarrinho(produtoNoCarrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public ReadCarrinhoDeComprasDto AlterarQuantidadeDeProdutosNoCarrinho(Guid carrinhoDeComprasId,
                                                                          UpdateCarrinhoDeComprasDto updateDto)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        var produtoNoCarrinho = _carrinhoDeComprasDAO
                                .GetProdutosNoCarrinho(carrinhoDeComprasId)
                                .FirstOrDefault(p => p.ProdutoId == updateDto.ProdutoId);

        if (produtoNoCarrinho == null)
            throw new NotFoundException("O Id de produto informado não está cadastrado no Id de carrinho informado.");

        if (produtoNoCarrinho.Quantidade == updateDto.Quantidade)
            throw new BadRequestException("A quantidade informada é a mesma cadastrada. Alteração não realizada.");

        if (updateDto.Quantidade == 0)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);
            return ReadCarrinhoDeCompras(carrinho);
        }

        var produto = _produtoService.PesquisarProdutoId(updateDto.ProdutoId);

        VerificaEstoqueEStatus(updateDto.Quantidade, produto);

        _mapper.Map(updateDto, produtoNoCarrinho);

        _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

        return ReadCarrinhoDeCompras(carrinho);
    }

    public ReadCarrinhoDeComprasDto GetCarrinho(Guid carrinhoDeComprasId)
    {
        var carrinho = _carrinhoDeComprasDAO.GetCarrinhoDeCompras(carrinhoDeComprasId);

        return ReadCarrinhoDeCompras(carrinho);
    }

    private ReadCarrinhoDeComprasDto ReadCarrinhoDeCompras(CarrinhoDeCompras carrinho)
    {
        var listaDeProdutos = new List<ResumoDoProduto>();

        var produtosNoCarrinho = _carrinhoDeComprasDAO.GetProdutosNoCarrinho(carrinho.Id);

        foreach (var item in produtosNoCarrinho)
        {
            listaDeProdutos.Add(new ResumoDoProduto
            {
                Id = item.ProdutoId,
                Nome = item.Produto.Nome,
                ValorUnitario = item.Produto.Valor.ToString("C"),
                Quantidade = item.Quantidade,
                Atencao = MensagemDeAtencao(item)
            });
        }

        var readCarrinhoDeCompras = _mapper.Map<ReadCarrinhoDeComprasDto>(carrinho);
        readCarrinhoDeCompras.Produtos.AddRange(listaDeProdutos);

        return readCarrinhoDeCompras;
    }

    private string? MensagemDeAtencao(ProdutoNoCarrinho produtoNoCarrinho)
    {

        if (produtoNoCarrinho.Produto.QuantidadeEmEstoque < 1)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);

            return "Este item não está mais disponível. O produto será retirado do carrinho.";
        }

        if (produtoNoCarrinho.Produto.QuantidadeEmEstoque < produtoNoCarrinho.Quantidade)
        {
            produtoNoCarrinho.Quantidade = (uint)produtoNoCarrinho.Produto.QuantidadeEmEstoque;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

            return $"O produto não possui o estoque disponível. A quantidade será ajustada para a quantidade disponível de {produtoNoCarrinho.Produto.QuantidadeEmEstoque} itens.";
        }

        if (produtoNoCarrinho.Produto.Status == false)
        {
            _carrinhoDeComprasDAO.RemoverProdutoDoCarrinho(produtoNoCarrinho);

            return "Este item não está mais disponível pois está inativo. O produto será retirado do carrinho.";
        }

        if (produtoNoCarrinho.ValorNoCarrinho > produtoNoCarrinho.Produto.Valor)
        {
            string message = $"O preço da unidade foi reduzido em {produtoNoCarrinho.ValorNoCarrinho - produtoNoCarrinho.Produto.Valor:C}.";

            produtoNoCarrinho.ValorNoCarrinho = produtoNoCarrinho.Produto.Valor;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);
            return message;
        }

        if (produtoNoCarrinho.ValorNoCarrinho < produtoNoCarrinho.Produto.Valor)
        {
            string message = $"O preço da unidade aumentou em {produtoNoCarrinho.Produto.Valor - produtoNoCarrinho.ValorNoCarrinho:C}.";

            produtoNoCarrinho.ValorNoCarrinho = produtoNoCarrinho.Produto.Valor;

            _carrinhoDeComprasDAO.AtualizarProdutoNoCarrinho(produtoNoCarrinho);

            return message;
        }

        return null;
    }

    private async Task<Endereco> EnderecoPorCep(string cep)
    {
        Log.Information("Iniciada pesquisa do CEP '{@cep}'.", cep);
        using (HttpClient client = new HttpClient())
        {
            var resposta = await client.GetStringAsync($"https://viacep.com.br/ws/{cep}/json/");
            Log.Information("Pesquisa do CEP finalizada. Resposta: {@resposta}", resposta);
            var endereco = JsonSerializer.Deserialize<Endereco>(resposta)!;

            return endereco.Erro == false
                                    ? endereco
                                    : throw new BadRequestException("CEP informado não é válido.");
        }
    }
}
