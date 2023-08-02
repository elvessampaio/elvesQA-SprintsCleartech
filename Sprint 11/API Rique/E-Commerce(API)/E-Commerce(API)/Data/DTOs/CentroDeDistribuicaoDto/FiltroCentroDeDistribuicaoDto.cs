using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;

public class FiltroCentroDeDistribuicaoDto
{
    [MinLength(3)]
    [MaxLength(128)]
    public string? Nome { get; set; }
    [MinLength(3)]
    [MaxLength(128)]
    public string? Logradouro { get; set; }
    [MinLength(3)]
    [MaxLength(128)]
    public string? Bairro { get; set; }
    [MinLength(3)]
    [MaxLength(128)]
    public string? Cidade { get; set; }
    [MinLength(2)]
    [MaxLength(2)]
    public string? UF { get; set; }
    [MinLength(8)]
    [MaxLength(8)]
    public string? CEP { get; set; }
    public Status Status { get; set; } = Status.Todos;
    public Ordem Ordem { get; set; } = Ordem.Crescente;
    public uint Registros { get; set; } = 30;
    public uint Pagina { get; set; } = 1;
}
