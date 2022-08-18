using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria.Controllers
{

    public class LoginController : Controller
    {
    
        public IActionResult Login()
        {
            return View();
        }
    }
}
