using E_Commerce_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_.Data;

public class ECommerceContext : DbContext
{
	public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
	{

	}

	public DbSet<Categoria> Categorias { get; set; }
	public DbSet<Subcategoria> Subcategorias { get; set; }
	public DbSet<Produto> Produtos { get; set; }
	public DbSet<CentroDeDistribuicao> CentrosDeDistribuicao { get; set; }
}
