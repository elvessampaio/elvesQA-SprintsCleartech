using E_Commerce_API_.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using E_Commerce_API_.Data;

namespace E_Commerce_API_.Models;

public class Categoria
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    [VerificacaoDoNome]
    public string Nome { get; set; }
    public bool Status { get; set; } = true;
    public DateTime DataEHoraCriacao { get; set; } = DateTime.Now;
    public DateTime DataEHoraModificacao { get; set; }
}
