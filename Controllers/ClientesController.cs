using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
       
        //Registro de los clientes
        public ClientesController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;
        [Authorize(Roles = "Administrador, Veterinario,Recepcionista")] //para el acceso de seguridad
        [HttpGet]
        public IActionResult ConsultarClientes(string buscar)
        {

            var clientes = from cliente in Database.Clientes select cliente;
            if (!string.IsNullOrEmpty(buscar))
            {
                clientes = clientes.Where(c => c.NombreDueño.Contains(buscar));
            }

            return View(clientes); //Envia los datos de los clientes como una lista
        }

        public IActionResult ConsultarClientes()
        {
            return View(Database.Clientes.ToList()); //Envia los datos de los clientes como una lista
        }

        [Authorize(Roles = "Administrador,Recepcionista")] //para el acceso de seguridad
        //Registrar un cliente
        [HttpGet]
        public IActionResult RegistrarCliente(int idCliente)
        {
            Clientes cliente = Database.Clientes.FirstOrDefault(x => x.IdCliente == idCliente);

            return View(cliente);


        }
        [HttpPost]

        public IActionResult RegistrarCliente(Clientes c)
        {
            Clientes cliente =
                c.IdCliente > 0
                ? Database.Clientes.FirstOrDefault(x => x.IdCliente == c.IdCliente)
                : new Clientes();
            if (cliente == null)
            { return NotFound(); }
            cliente.IdCliente = c.IdCliente;
            cliente.Cedula = c.Cedula;
            cliente.NombreDueño = c.NombreDueño;
            cliente.NombreMascota = c.NombreMascota;
            cliente.RazaMascota = c.RazaMascota;
            cliente.TipoMascota = c.TipoMascota;
            cliente.Celular = c.Celular;
            cliente.Correo = c.Correo;
            {
                Database.Clientes.Add(cliente);
            }
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarClientes));
        }

        [Authorize(Roles = "Administrador,Recepcionista")] //para el acceso de seguridad
        [HttpGet]
        public IActionResult EditarCliente(int idCliente)
        {
            Clientes cliente = Database.Clientes.Where(x => x.IdCliente == idCliente).FirstOrDefault();

            return View(cliente);
        }
        [HttpPost]

        public IActionResult EditarCliente(Clientes c)
        {
            Database.Attach(c);
            Database.Entry(c).State = EntityState.Modified;
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarClientes));
        }

        [HttpGet]
        public IActionResult EliminarCliente(int idCliente)
        {
            Clientes cliente = Database.Clientes.Where(x => x.IdCliente == idCliente).FirstOrDefault();

            return View(cliente);
        }
        [HttpPost]
        public IActionResult EliminarCliente(Clientes c)
        {
            Database.Attach(c);
            Database.Entry(c).State = EntityState.Deleted;
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarClientes));
        }
    }
}