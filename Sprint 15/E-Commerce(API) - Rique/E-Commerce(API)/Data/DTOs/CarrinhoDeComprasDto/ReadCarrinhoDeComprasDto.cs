using E_Commerce_API_.Models;

namespace E_Commerce_API_.Data.DTOs.CarrinhoDeComprasDto;

public class ReadCarrinhoDeComprasDto
{
    public ReadCarrinhoDeComprasDto()
    {
        Produtos = new List<ResumoDoProduto>();
    }

    public Guid Id { get; set; }
    public List<ResumoDoProduto> Produtos { get; set; }
    public string ValorTotal { get; set; }
    public string Logradouro { get; set; }
    public uint Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
}

public class ResumoDoProduto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string ValorUnitario { get; set; }
    public uint Quantidade { get; set; }
    public string? Atencao { get; set; }


    public bool ShouldSerializeAtencao()
    {
        return Atencao != null;
    }
}
