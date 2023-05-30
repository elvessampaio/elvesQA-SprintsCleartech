using E_Commerce_API_.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Data.DTOs;

public class CreateCategoriaDto
{
    [Required]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataEHoraCriacao { get; set; } = DateTime.Now;
}
