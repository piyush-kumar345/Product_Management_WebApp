using Microsoft.AspNetCore.Mvc;

namespace Product_Management_WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();  
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
