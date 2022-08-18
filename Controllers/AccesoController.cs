using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProyectoVeterinaria.Models
{
    public class AccesoController : Controller
    {
        public AccesoController(ApplicationDbContext database)
        {
            Database = database;
        }

        public ApplicationDbContext Database;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Empleado usuario)
        {

            Empleado cliente =
        usuario.Email != null
        ? Database.Empleados.FirstOrDefault(x => x.Email == usuario.Email && x.contra == usuario.contra)
        : new Empleado();

            if (cliente != null)

            {
                //se crea la cookie
                var claims = new List<Claim>
                {    
                    //se almacena los datos
                    new Claim(ClaimTypes.Name, cliente.contra),
                    new Claim("Email", cliente.Email),
                    new Claim(ClaimTypes.Role, cliente.Roles)

                };

              

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //se crea la cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "login");
            }

        }

        public async Task<IActionResult> salir()
        {
            //elimina la cookie creada
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "login");
        }

    }
}
