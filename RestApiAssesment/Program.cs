using Microsoft.EntityFrameworkCore;
using RestApiAssesment.Data;
using RestApiAssesment.Repositories;
using RestApiAssesment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(Option =>
Option.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
// Singleton for ID generation
builder.Services.AddSingleton<ProductIdGeneratorService>(); 



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
