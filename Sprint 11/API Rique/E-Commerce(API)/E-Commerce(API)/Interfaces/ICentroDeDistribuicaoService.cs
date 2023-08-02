using E_Commerce_API_.Data.DTOs;
using E_Commerce_API_.Data.DTOs.CentroDeDistribuicaoDto;
using E_Commerce_API_.Models;
using FluentResults;

namespace E_Commerce_API_.Interfaces;

public interface ICentroDeDistribuicaoService
{
    public Task<Result<ReadCentroDeDistribuicaoDto>> AdicionarCD(CreateCentroDeDistribuicaoDto centroDeDistribuicaoDto);
    public Task<Result> EditarCD(Guid id, UpdateCentroDeDistribuicaoDto centroDeDistribuicaoDto);
    public CentroDeDistribuicao? BuscarCDPorId(Guid id);
    Result DeletarCD(Guid id);
    Task<List<ReadCentroDeDistribuicaoDto>> ListaFiltrada(FiltroCentroDeDistribuicaoDto filtro);
}
