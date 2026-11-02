using System.ComponentModel.DataAnnotations;

namespace blazorwa.Models;

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
    public string? Descripcion { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }
}
