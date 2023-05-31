using Ecommerce_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//o builder serve para fazer a conexão ao banco

var connectionString = builder.Configuration.GetConnectionString("EcommerceAPIConnection");

builder.Services.AddDbContext<CategoriaContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder para utilizar o AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
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
