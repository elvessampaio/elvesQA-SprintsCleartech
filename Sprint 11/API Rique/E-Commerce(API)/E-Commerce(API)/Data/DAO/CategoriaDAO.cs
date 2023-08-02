using E_Commerce_API_.Data.DTOs.CategoriaDto;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace E_Commerce_API_.Data.DAO;

public class CategoriaDAO : ICategoriaDAO
{
    private ECommerceContext _context;
    private readonly IConfiguration _configuration;
    private readonly string? connectionSring;

    public CategoriaDAO(ECommerceContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        connectionSring = _configuration.GetConnectionString("ECommerceConnection");
    }

    public void Add(Categoria c)
    {
        _context.Categorias.Add(c);
        _context.SaveChanges();
    }

    public void Update(Categoria c)
    {
        _context.Categorias.Update(c);
        _context.SaveChanges();
    }

    public void Delete(Categoria c)
    {
        _context.Remove(c);
        _context.SaveChanges();
    }

    public List<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public async Task<IEnumerable<Categoria>> GetListaCategorias(FiltroCategoriaDto filtro)
    {
        string querie = "SELECT * FROM `E-COMMERCE(API)`.CATEGORIAS WHERE ";

        if (filtro.Nome != null)
        {
            querie += $"LOCATE ('{filtro.Nome}', NOME) AND ";
        }

        switch (filtro.Status)
        {
            case Status.Inativo:
                querie += $"STATUS = FALSE ";
                break;

            case Status.Ativo:
                querie += $"STATUS = TRUE ";
                break;

            case Status.Todos:
                querie += "1=1 ";
                break;
        }

        if (filtro.Ordem == Ordem.Crescente)
            querie += "ORDER BY NOME ";
        else if (filtro.Ordem == Ordem.Decrescente)
            querie += "ORDER BY NOME DESC ";

        querie += $"LIMIT {(filtro.Pagina - 1) * filtro.Registros}, {filtro.Registros}";

        using (var conection = new MySqlConnection(connectionSring))
        {
            return await conection.QueryAsync<Categoria>(querie);
        }
    }
}