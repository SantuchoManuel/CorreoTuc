using System.Net.Http.Json;
using blazorwa.Models;

namespace blazorwa.Services;

public class ProductoService(HttpClient http)
{
    private const string Ruta = "api/productos";

    public async Task<List<Producto>> ObtenerTodosAsync() =>
        await http.GetFromJsonAsync<List<Producto>>(Ruta) ?? [];

    public Task<Producto?> ObtenerPorIdAsync(int id) =>
        http.GetFromJsonAsync<Producto>($"{Ruta}/{id}");

    public async Task<Producto?> CrearAsync(Producto producto)
    {
        var respuesta = await http.PostAsJsonAsync(Ruta, producto);
        respuesta.EnsureSuccessStatusCode();
        return await respuesta.Content.ReadFromJsonAsync<Producto>();
    }

    public async Task ActualizarAsync(Producto producto)
    {
        var respuesta = await http.PutAsJsonAsync($"{Ruta}/{producto.Id}", producto);
        respuesta.EnsureSuccessStatusCode();
    }

    public async Task EliminarAsync(int id)
    {
        var respuesta = await http.DeleteAsync($"{Ruta}/{id}");
        respuesta.EnsureSuccessStatusCode();
    }
}
