using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoVeterinaria.Models
{
    public class CitasViewModel
    {
        public CitasViewModel()
        {
            Cliente = new List<SelectListItem>();
            Nombre = new List<SelectListItem>();
            Raza = new List<SelectListItem>();
            Empleado = new List<SelectListItem>();

        }
        public Citas Cita { get; set; }
        public IEnumerable<SelectListItem> Cliente { get; set; }
        public IEnumerable<SelectListItem> Nombre { get; set; }
        public IEnumerable<SelectListItem> Raza { get; set; }
        public IEnumerable<SelectListItem> Empleado { get; set; }

    }
}