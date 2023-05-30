using E_Commerce_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_.Data;

public class CategoriaContext : DbContext
{
	public CategoriaContext(DbContextOptions<CategoriaContext> options) : base(options)
	{

	}

	public DbSet<Categoria> Categorias { get; set; }
}
