using E_Commerce_API_.Data;
using E_Commerce_API_.Data.DAO;
using E_Commerce_API_.Interfaces;
using E_Commerce_API_.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ECommerceConnection");

builder.Services.AddDbContext<ECommerceContext>(options => options
    .UseLazyLoadingProxies()
    .UseMySql(builder.Configuration
    .GetConnectionString("ECommerceConnection"), ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaDAO, CategoriaDAO>();
builder.Services.AddScoped<ISubcategoriaService, SubcategoriaService>();
builder.Services.AddScoped<ISubcategoriaDAO, SubcategoriaDAO>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoDAO, ProdutoDAO>();
builder.Services.AddScoped<ICentroDeDistribuicaoService, CentroDeDistribuicaoService>();
builder.Services.AddScoped<ICentroDeDistribuicaoDAO, CentroDeDistribuicaoDAO>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public enum Status { Inativo, Ativo, Todos }
public enum Ordem { Crescente, Decrescente }