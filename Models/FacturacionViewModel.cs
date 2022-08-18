using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoVeterinaria.Models
{
    public class FacturacionViewModel
    {
        public FacturacionViewModel()
        {
            Cedula = new List<SelectListItem>();
            CedulaEmpleado = new List<SelectListItem>();
            Producto = new List<SelectListItem>();
            ProductoPrecio = new List<SelectListItem>();

        }

        public FacturaDetalle FacturaDetalle { get; set; }
        public Facturacion Facturacion { get; set; }
        public IEnumerable<SelectListItem> Cedula { get; set; }

        public IEnumerable<SelectListItem> CedulaEmpleado { get; set; }

        public IEnumerable<SelectListItem> Producto { get; set; }
        public IEnumerable<SelectListItem> ProductoPrecio { get; set; }

        public int IdFacturaDetalle { get; set; }
        
        public int Cantidad { get; set; }
        public double Total { get; set; }

    }


}
