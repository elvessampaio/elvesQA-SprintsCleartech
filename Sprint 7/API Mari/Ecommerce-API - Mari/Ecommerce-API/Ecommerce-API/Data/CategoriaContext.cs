using Ecommerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Data;

//essa classe é um contexto para acessar o banco de dados. É uma forma de relacionar o código com o banco de dados
public class CategoriaContext : DbContext // a classe herda do DbContexte
{
    //esse construtor vai receber uma configuração que são as opções de acesso ao banco deste contexto
    public CategoriaContext(DbContextOptions<CategoriaContext> opts) : base(opts)
    {
        
    }

    //essa será a propriedade de acesso das Categorias na base
    //DbSet é um conjunto de dados
    public DbSet<Categoria> Categorias { get; set; }
}
