using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
      
        //parte para el registro de los empleados
        public EmpleadosController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;

        public IActionResult Index()

        {
           
            return View(Database.Empleados.ToList()); //manda los datos como lista

        }
        //filtro
        [HttpGet]
        public IActionResult Index(string Nombre)
        {

            var Empleados = from Empleado in Database.Empleados select Empleado;
            if (!string.IsNullOrEmpty(Nombre))
            {
                Empleados = Empleados.Where(c => c.Nombre.Contains(Nombre));
            }

            return View(Empleados);
        }

        //registro de un nuevo empleado
        [HttpGet]
        public IActionResult RegistrarE(int id)
        {

            EmpleadoViewModel model = new EmpleadoViewModel();
            model.Empleado = Database.Empleados.FirstOrDefault(x => x.Id == id);
            //llamado del comobox
            model.Puestos = new SelectList(Database.Puestos, "id", "puesto");
            return View(model);
        }
        [HttpPost]
        public IActionResult RegistrarE(EmpleadoViewModel c)
        {
            //llamado de la BD
            Empleado emp =
                c.Empleado.Id > 0
                ? Database.Empleados.FirstOrDefault(x => x.Id == c.Empleado.Id)
                : new Empleado();
            if (emp == null)
            { return NotFound(); }
            emp.Id = c.Empleado.Id;
            emp.cedula = c.Empleado.cedula;
            emp.Nombre = c.Empleado.Nombre;
            emp.ApellIdempleadoos = c.Empleado.ApellIdempleadoos;
            emp.Email = c.Empleado.Email;
            emp.contra = c.Empleado.contra;
            emp.Estado = c.Empleado.Estado;
            emp.PuestoID = c.Empleado.PuestoID;
            emp.Roles = c.Empleado.Roles;
            if (emp.Id == 0)
            {
                Database.Empleados.Add(emp);
            }
            Database.SaveChanges();
            TempData["mensaje"] = "Se registró con exito";
            return RedirectToAction(nameof(Index));
        }

        //para poder editar el empleado
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            EmpleadoViewModel model = new EmpleadoViewModel();
            model.Empleado = Database.Empleados.FirstOrDefault(x => x.Id == id);

            //llamado del comobox
            model.Puestos = new SelectList(Database.Puestos, "id", "puesto");
            return View(model);
        }

        //modificar 

        [HttpPost]
        public IActionResult Editar(EmpleadoViewModel c)
        {

            Database.Empleados.Update(c.Empleado);
            Database.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


    }
}
