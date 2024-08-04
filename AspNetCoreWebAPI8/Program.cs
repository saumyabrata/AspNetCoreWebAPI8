using AspNetCoreWebAPI8.Models;
using AspNetCoreWebAPI8.Repository;
using AspNetCoreWebAPI8.Service;
using Microsoft.EntityFrameworkCore;

//create a builder container
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<ProdContext>(options =>
//                    options.UseSqlServer(builder.Configuration.GetConnectionString("ProdContextConnString")));
//builder.Services.AddDbContext<ProdContext>(options =>
//	options.UseSqlServer("ProdContextConnString"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
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
