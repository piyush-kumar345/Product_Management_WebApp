using Microsoft.AspNetCore.Mvc;

namespace Product_Management_WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();  
        }
    }
}
