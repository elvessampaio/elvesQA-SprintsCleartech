﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Data.DTOs.ProdutoDto;

public class FiltroProdutoDto
{
    [MinLength(3)]
    [MaxLength(128)]
    public string? Nome { get; set; }
    public double? Peso { get; set; }
    public double? Altura { get; set; }
    public double? Largura { get; set; }
    public double? Comprimento { get; set; }
    public double? Valor { get; set; }
    public uint? Estoque { get; set; }
    public string? CentroDeDistribuicaoId { get; set; }
    public string? CategoriaId { get; set; }
    public string? SubcategoriaId { get; set; }
    public Status Status { get; set; } = Status.Ativo;
    public Ordem Ordem { get; set; } = Ordem.Crescente;
    public uint Registros { get; set; } = 30;
    public uint Pagina { get; set; } = 1;
}
