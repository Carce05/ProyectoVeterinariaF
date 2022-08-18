using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria.Models;

namespace ProyectoVeterinaria.Controllers
{
   
    public class CitasController : Controller
    {
        //Registro de Citas
     
        public CitasController(ApplicationDbContext database, IConfiguration configuration, ICartero cartero)
        {
            Configuration = configuration;
            Cartero = cartero;
            Database = database;
        }

        IConfiguration Configuration { get; }
        ICartero Cartero { get; }

        public IActionResult EnviarCorreo()
        {
            CorreoElectronico correo =
                new CorreoElectronico
                {
                    Destinatario = "mariafernandah527@gmail.com",
                    Asunto = "Recordatorio de Cita",
                    Cuerpo = "Hola muy buenas noches. Se le recuerda que su cita con el Dr.Fernandez Granados es el dia miercoles" +
                    "27 de julio a las 2:00 p.m. Me podria confirmar si asistira a la cita. Muchas gracias"
                };
            Cartero.Enviar(correo);
            return RedirectToAction(nameof(ConsultarCitas));
        }

        public ApplicationDbContext Database;


        [Authorize(Roles = "Administrador, Veterinario,Recepcionista")]
        public IActionResult ConsultarCitas()
        {
            return View(Database.Citas.ToList()); //Envia los datos de la cita como una lista
        }

    
        [HttpGet]
        public IActionResult ConsultarCitas(string buscar)
        {

            var citas = from cita in Database.Citas select cita;
            if (!string.IsNullOrEmpty(buscar))
            {
                citas = citas.Where(c => c.NombreDueño.Contains(buscar));
            }

            return View(citas);
        }

       
        [Authorize(Roles = "Administrador, Recepcionista")]
        //Agendar una cita
        [HttpGet]
        public IActionResult AgendarCita(int idCita)
        {
            CitasViewModel model = new CitasViewModel();
            model.Cita = Database.Citas.FirstOrDefault(x => x.IdCita == idCita);
            model.Cliente = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.Nombre = new SelectList(Database.Clientes, "IdCliente", "NombreDueño");
            model.Raza = new SelectList(Database.Clientes, "IdCliente", "RazaMascota");
            model.Empleado = new SelectList(Database.Empleados, "Id","cedula");


            return View(model);

        }

     
        [HttpPost]
        public IActionResult AgendarCita(CitasViewModel c)
        {
            //Se llama la Base de Datos
            Citas cita =
                c.Cita.IdCita > 0
                ? Database.Citas.FirstOrDefault(x => x.IdCita == c.Cita.IdCita)
                : new Citas();
            if (cita == null)
            { return NotFound(); }
            cita.IdCita = c.Cita.IdCita;
            cita.ClienteIdCliente = c.Cita.ClienteIdCliente;
            cita.EmpleadoId = c.Cita.EmpleadoId;
            cita.NombreDueño = c.Cita.NombreDueño;
            cita.NombreVeterinario = c.Cita.NombreVeterinario;
            cita.RazaMascota = c.Cita.RazaMascota;
            cita.EstadoMascota = c.Cita.EstadoMascota;
            cita.FechaCita = c.Cita.FechaCita;
            cita.HoraCita = c.Cita.HoraCita;
            cita.MotivoCita = c.Cita.MotivoCita;
            cita.NombreServicio = c.Cita.NombreServicio;

            if (cita.IdCita == 0)
            {
                Database.Citas.Add(cita);
            }
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarCitas));
        }

        [Authorize(Roles = "Administrador, Recepcionista")]
        [HttpGet]
        public IActionResult EditarCita(int? idCita)
        {
            CitasViewModel model = new CitasViewModel();
            model.Cita = Database.Citas.FirstOrDefault(x => x.IdCita == idCita);
            //Llamados de las listas desplegables
            model.Cliente = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.Nombre = new SelectList(Database.Clientes, "IdCliente", "NombreDueño");
            model.Raza = new SelectList(Database.Clientes, "IdCliente", "RazaMascota");
            model.Empleado = new SelectList(Database.Empleados, "Id", "cedula");
            return View(model);
        }

        //Modificar una Cita
    
        [HttpPost]
        public IActionResult EditarCita(CitasViewModel c)
        {
            Database.Citas.Update(c.Cita);
            Database.SaveChanges();
            TempData["Mensaje"] = "La Cita se modifico con exito";
            return RedirectToAction(nameof(ConsultarCitas));
        }

        [Authorize(Roles = "Administrador, Recepcionista")]
        [HttpGet]
        public IActionResult EliminarCita(int? idCita)
        {
            CitasViewModel model = new CitasViewModel();
            model.Cita = Database.Citas.FirstOrDefault(x => x.IdCita == idCita);
            //Llamados de las listas desplegables
            model.Cliente = new SelectList(Database.Clientes, "IdCliente", "Cedula");
            model.Nombre = new SelectList(Database.Clientes, "IdCliente", "NombreDueño");
            model.Raza = new SelectList(Database.Clientes, "IdCliente", "RazaMascota");
            model.Empleado = new SelectList(Database.Empleados, "Id", "cedula");
            return View(model);
        }

        //Modificar una Cita

        [HttpPost]
        public IActionResult EliminarCita(CitasViewModel c)
        {


            Database.Citas.Remove(c.Cita);
            Database.SaveChanges();
            return RedirectToAction(nameof(ConsultarCitas));



        }
      
      
    }

}