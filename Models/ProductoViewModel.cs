using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoVeterinaria.Models
{
    public class ProductoViewModel
    {
        public ProductoViewModel()
        {
            Proveedores = new List<SelectListItem>();
        }
        public Producto Producto { get; set; }
        public IEnumerable<SelectListItem> Proveedores { get; set; }

    }
}
