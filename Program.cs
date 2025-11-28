
using AspNetCore.Scalar;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using RestApi.Interfaces;
using RestApi.Models;
using RestApi.Repo;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Rate Limiter
builder.Services.AddRateLimiter(ratelimiterOptions =>
{
    ratelimiterOptions.AddFixedWindowLimiter("Fixed", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromSeconds(5);
        options.QueueLimit = 2;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
    ratelimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

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

    // http://localhost:5101/scalar-api-docs/
    app.UseScalar(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
        options.DocumentTitle = "Training API ASP.NET Core 10 - Scalar UI";
    });

    // http://localhost:5000/scalar/v1 
    app.MapScalarApiReference(
        options =>
        {
            options.WithTitle("Training API ASP.NET Core 10 - Scalar UI")
                    .WithTheme(ScalarTheme.BluePlanet)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Axios);
        }
    );
}

app.UseHttpsRedirection();

// rate limiter gunakan 
app.UseRateLimiter();

// controllers
app.MapGet("/ratelimit", () => "Hello Json").RequireRateLimiting("Fixed");
app.MapGet("/nolimit", () => "Tanpa Limit");

app.MapControllers();

app.Run();


