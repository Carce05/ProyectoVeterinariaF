using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoVeterinaria.Models
{
    public class EmpleadoViewModel
    {

        public EmpleadoViewModel()
        {

            Puestos = new List<SelectListItem>();
        }

        public Empleado Empleado{ get; set; }
        public Puesto EPuesto { get; set; }
        public IEnumerable<SelectListItem> Puestos { get; set; }

    }
}
