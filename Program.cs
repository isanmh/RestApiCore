
using Microsoft.EntityFrameworkCore;
using RestApi.Interfaces;
using RestApi.Models;
using RestApi.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// koneksi database 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

// Agar tidak Looping Reference ketika mengembalikan data dengan relasi
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// instalasi service controller
builder.Services.AddControllers();

// Servide Dependency Injection (DI) buatan kita
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// controllers
app.MapControllers();

app.Run();


