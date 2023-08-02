using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_.Interfaces;

public interface ICategoriaService
{
    Task<List<ReadCategoriaDto>> ListaFiltrada(FiltroCategoriaDto filtro);
    Result<ReadCategoriaDto> AdicionarCategoria(CreateCategoriaDto categoriaDto);
    Result AtualizarCategoria(Guid id, [FromBody] UpdateCategoriaDto categoriaDto);
    Result RemoverCategoria(Guid id);
}
