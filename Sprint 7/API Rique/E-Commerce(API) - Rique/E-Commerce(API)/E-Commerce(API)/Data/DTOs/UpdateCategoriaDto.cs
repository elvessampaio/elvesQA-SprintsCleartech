using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Data.DTOs;

public class UpdateCategoriaDto
{
    [Required]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Status deve ser definido como false(inativo) ou true(ativo).")]
    public bool Status { get; set; }
}
