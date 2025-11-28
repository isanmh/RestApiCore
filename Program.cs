
using AspNetCore.Scalar;


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

// Service Dependency Injection (DI) buatan kita
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // swagger ui
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Training API ASP.NET Core 10");
    });

    // Scalar UI
    app.UseScalar(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
        options.DocumentTitle = "Training API ASP.NET Core 10 - Scalar UI";
    });
}

app.UseHttpsRedirection();

// controllers
app.MapControllers();

app.Run();


