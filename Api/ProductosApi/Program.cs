using ProductosApi.Data;
using ProductosApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductoRepository>();
builder.Services.AddValidation();
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy => policy
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [])
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("BlazorClient");

var productos = app.MapGroup("/api/productos");

productos.MapGet("/", (ProductoRepository repo) => TypedResults.Ok(repo.ObtenerTodos()));

productos.MapGet("/{id:int}", (int id, ProductoRepository repo) =>
    repo.ObtenerPorId(id) is { } producto
        ? Results.Ok(producto)
        : Results.NotFound());

productos.MapPost("/", (Producto producto, ProductoRepository repo) =>
{
    var creado = repo.Agregar(producto);
    return TypedResults.Created($"/api/productos/{creado.Id}", creado);
});

productos.MapPut("/{id:int}", (int id, Producto producto, ProductoRepository repo) =>
    repo.Actualizar(id, producto) ? Results.NoContent() : Results.NotFound());

productos.MapDelete("/{id:int}", (int id, ProductoRepository repo) =>
    repo.Eliminar(id) ? Results.NoContent() : Results.NotFound());

app.Run();
