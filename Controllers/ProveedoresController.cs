using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
    [Authorize]
    public class ProveedoresController : Controller
    {
       
        //Registro de los proveedores
        public ProveedoresController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;

        [HttpGet]
        public IActionResult BuscarProveedores(string buscar)
        {
            IQueryable<Proveedor> proveedoresEncontrados;

            var proveedores = from proveedor in Database.Proveedor select proveedor;
            if (!string.IsNullOrEmpty(buscar))
            {
                proveedoresEncontrados = proveedores.Where(c => c.nombre.Contains(buscar) || c.cedula.ToString().Contains(buscar));

                if (proveedoresEncontrados.Count() > 0)
                {
                    proveedores = proveedoresEncontrados;
                    TempData["EncontroProveedores"] = true;
                }
                else
                {
                    @TempData["Mensaje"] = "No se encontrarón proveedores para \"" + buscar + "\"";
                }
            }

            return View("ConsultarProveedores",proveedores); //Envia los datos de los proveedores como una lista
        }

        [HttpGet]
        public IActionResult ConsultarProveedores()
        {
            return View(Database.Proveedor.ToList()); //Envia los datos de los clientes como una lista
        }

        [HttpGet]
        public IActionResult RegistrarProveedores()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditarProveedores(int idProveedor)
        {
            //Devuelve el proveedor que corresponda con el id de proveedor recibido
            return View(Database.Proveedor.ToList().FirstOrDefault(proveedor => proveedor.idProveedor == idProveedor));
        }

        [HttpGet]
        public IActionResult EliminarProveedores(int idProveedor)
        {
            //Devuelve el proveedor que corresponda con el id de proveedor recibido
            return View(Database.Proveedor.ToList().FirstOrDefault(proveedor => proveedor.idProveedor == idProveedor));
        }

        //Registrar un proveedor
        [HttpPost]
        public IActionResult RegistrarProveedor(Proveedor c)
        {
            Proveedor proveedor =
                c.idProveedor > 0
                ? Database.Proveedor.FirstOrDefault(x => x.idProveedor == c.idProveedor)
                : new Proveedor();
            if (proveedor == null)
            { return NotFound(); }
            proveedor.idProveedor = c.idProveedor;
            proveedor.nombre = c.nombre;
            proveedor.telefono = c.telefono;
            proveedor.cedula = c.cedula;
            proveedor.direccion = c.direccion;
            proveedor.correo = c.correo;
            {
                Database.Proveedor.Add(proveedor);
            }
            Database.SaveChanges();
            TempData["Mensaje"] = "El Proveedor se registro con exito";
            return RedirectToAction(nameof(ConsultarProveedores));
        }
       
        [HttpPost]
        public IActionResult EditarProveedor(Proveedor c)
        {
            Database.Attach(c);
            Database.Entry(c).State = EntityState.Modified;
            Database.SaveChanges();
            TempData["Mensaje"] = "El Proveedor se modifico con exito";
            return RedirectToAction(nameof(ConsultarProveedores));
        }

        [HttpPost]
        public IActionResult EliminarProveedor(Proveedor c)
        {
            Database.Attach(c);
            Database.Entry(c).State = EntityState.Deleted;
            Database.SaveChanges();
            TempData["Mensaje"] = "El Proveedor se elimino con exito";
            return RedirectToAction(nameof(ConsultarProveedores));
        }
    }
}