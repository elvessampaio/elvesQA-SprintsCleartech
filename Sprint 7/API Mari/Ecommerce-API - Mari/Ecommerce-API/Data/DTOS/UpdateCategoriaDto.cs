using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS;

//DTO é um padrão de projeto usado para transportar dados entre diferentes componentes de um sistema,
//diferentes instâncias ou processos de um sistema distribuído ou diferentes sistemas via serialização
public class UpdateCategoriaDto
{
  
    //O required faz o campo ser obrigatório
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get; set; }
    public bool Status { get; set; }
    public DateTime DataModificacao { get; set; } = DateTime.Now;

}
