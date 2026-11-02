using System.Collections.Concurrent;
using ProductosApi.Models;

namespace ProductosApi.Data;

public class ProductoRepository
{
    private readonly ConcurrentDictionary<int, Producto> _productos = new();
    private int _ultimoId;

    public ProductoRepository()
    {
        Agregar(new Producto { Nombre = "Teclado", Descripcion = "Teclado mecánico", Precio = 59.90m, Stock = 15 });
        Agregar(new Producto { Nombre = "Mouse", Descripcion = "Mouse inalámbrico", Precio = 24.50m, Stock = 40 });
        Agregar(new Producto { Nombre = "Monitor", Descripcion = "Monitor 27\" 4K", Precio = 329.00m, Stock = 8 });
    }

    public IEnumerable<Producto> ObtenerTodos() => _productos.Values.OrderBy(p => p.Id);

    public Producto? ObtenerPorId(int id) => _productos.TryGetValue(id, out var p) ? p : null;

    public Producto Agregar(Producto producto)
    {
        producto.Id = Interlocked.Increment(ref _ultimoId);
        _productos[producto.Id] = producto;
        return producto;
    }

    public bool Actualizar(int id, Producto producto)
    {
        if (!_productos.ContainsKey(id)) return false;
        producto.Id = id;
        _productos[id] = producto;
        return true;
    }

    public bool Eliminar(int id) => _productos.TryRemove(id, out _);
}
