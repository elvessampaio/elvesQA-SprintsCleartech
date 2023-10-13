using System.ComponentModel.DataAnnotations;

namespace Gerardor_de_Cupons_API__.Data;

public class CreateCupom
{
    [Required]
    public string TipoDeDesconto { get; set; }
    [Required]
    public double Valor { get; set; }
}
