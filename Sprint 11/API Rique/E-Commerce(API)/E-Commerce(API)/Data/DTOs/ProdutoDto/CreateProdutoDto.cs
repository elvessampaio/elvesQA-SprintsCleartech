using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class CreateProdutoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(ErrorMessage = "O Nome do produto é obrigatório.")]
    [StringLength(128)]
    [RegularExpression(caracteresPermitidos, ErrorMessage = "O campo deve ser alfanumérico.")]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [Required(ErrorMessage = "Insira uma descrição para o produto.")]
    [StringLength(512)]
    [RegularExpression(caracteresPermitidos, ErrorMessage = "O campo deve ser alfanumérico.")]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o peso do produto.")]
    public double Peso { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para a altura do produto.")]
    public double Altura { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para a largura do produto.")]
    public double Largura { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o comprimento do produto.")]
    public double Comprimento { get; set; }
    [Required(ErrorMessage = "Insira um valor decimal para o valor do produto.")]
    public double Valor { get; set; }
    [Required(ErrorMessage = "Insira a quantidade de produtos no estoque para cadastro.")]
    public int QuantidadeEmEstoque { get; set; }
    [Required(ErrorMessage = "Insira o ID Centro de Distribuição em que o produto se encontra.")]
    public Guid CentroDeDistribuicaoId { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataEHoraCriacao { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Insira o ID da Subcategoria em que o produto se encontra.")]
    public Guid SubcategoriaId { get; set; }
}
