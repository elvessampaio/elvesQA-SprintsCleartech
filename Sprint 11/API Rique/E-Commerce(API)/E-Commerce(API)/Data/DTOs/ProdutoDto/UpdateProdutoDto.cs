using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class UpdateProdutoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;

    [StringLength(128)]
    [RegularExpression(caracteresPermitidos, ErrorMessage = "O campo deve ser alfanumérico.")]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    [StringLength(512)]
    [RegularExpression(caracteresPermitidos, ErrorMessage = "O campo deve ser alfanumérico.")]
    public string Descricao { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double Largura { get; set; }
    public double Comprimento { get; set; }
    public double Valor { get; set; }
    public int QuantidadeEmEstoque { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraModificacao { get; set; } = DateTime.Now;
}
