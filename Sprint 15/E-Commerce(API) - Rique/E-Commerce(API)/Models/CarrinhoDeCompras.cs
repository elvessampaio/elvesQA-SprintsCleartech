using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Models;

public class CarrinhoDeCompras
{
    public CarrinhoDeCompras()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
    public double ValorTotal { get => TotalDoCarrinho(); }
    public string Logradouro { get; set; }
    public uint Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public virtual IEnumerable<ProdutoNoCarrinho> Produtos { get; set; }

    private double TotalDoCarrinho()
    {
        double _valorTotal = 0;
        var produtos = this.Produtos.ToList();
        foreach (var produto in produtos)
        {
            _valorTotal += produto.Produto.Valor * produto.Quantidade;
        }
        return _valorTotal;
    }
}
