using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
    [Authorize]
    public class PuestoController : Controller
    {
        
        public PuestoController(ApplicationDbContext database)
        {
            Database = database;
        }


        public ApplicationDbContext Database;

        public IActionResult Index()
        {
            return View(Database.Puestos.ToList());
        }

        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Puesto pues = Database.Puestos.FirstOrDefault(x => x.id == id);

            return View(pues);
        }
        [HttpPost]
        public IActionResult Upsert(Puesto c)
        {
            Puesto pues =
                c.id > 0
                ? Database.Puestos.FirstOrDefault(x => x.id == c.id)
                : new Puesto();
            if (pues == null)
            { return NotFound(); }
            pues.id = c.id;
            pues.puesto = c.puesto;

            if (pues.id == 0)
            {
                Database.Puestos.Add(pues);
            }
            Database.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //para poder editar 
        [HttpGet]
        public IActionResult EditarPuesto(int? id)
        {
            Puesto model = new Puesto();
            Puesto? puesto = Database.Puestos.FirstOrDefault(x => x.id == id);
            //llamado del comobox
            return View(puesto);
        }

        //modificar 

        [HttpPost]
        public IActionResult Editar(EmpleadoViewModel c)
        {


            Database.Puestos.Update((Puesto)c.Puestos);
            Database.SaveChanges();
            TempData["mensaje"] = "Se editó con exito";
            return RedirectToAction(nameof(Index));



        }
    }
}
