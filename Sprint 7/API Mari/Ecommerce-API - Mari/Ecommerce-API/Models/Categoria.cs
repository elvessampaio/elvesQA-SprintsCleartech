using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Models;

//Aqui é o modelo mapeado da vida real para a linguagem .NET
public class Categoria
{
    private string _nome;

    [Key]
    [Required]
    public int Id { get; set; }

    //O required faz o campo ser obrigatório
    [Required(ErrorMessage = "O campo deve ser obrigatório")]
    [StringLength(128, ErrorMessage = "O campo deve conter até 128 caracteres")]
    [RegularExpression("^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$", ErrorMessage = "O campo deve conter apenas caracteres do alfabeto.")]
    public string Nome { get { return _nome; } set { _nome = value.ToUpper(); } } //transformar em maiúscula todo valor do Nome que receber
    public bool Status { get; set; } 
    public DateTime DataCriacao { get; set; }
    public DateTime DataModificacao { get; set; }
  
}
