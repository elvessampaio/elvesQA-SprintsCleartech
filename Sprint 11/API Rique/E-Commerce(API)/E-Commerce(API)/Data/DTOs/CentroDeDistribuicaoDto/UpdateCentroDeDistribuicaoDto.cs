using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;

public class UpdateCentroDeDistribuicaoDto
{
    private const string caracteresPermitidos = "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s0-9]+$";
    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
    private string _nome;

    [MinLength(3)]
    [MaxLength(128)]
    [RegularExpression(caracteresPermitidos, ErrorMessage = "O campo deve ser alfanumérico.")]
    public string Nome { get { return _nome; } set { _nome = ti.ToTitleCase(value.ToLower()); } }
    public string? Logradouro { get; set; }
    [Required]
    public uint Numero { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(128)]
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? UF { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(8)]
    public string CEP { get; set; }
    public bool Status { get; set; }
    public DateTime DataEHoraModificacao { get; set; } = DateTime.Now;
}
