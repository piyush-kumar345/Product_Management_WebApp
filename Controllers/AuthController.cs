using Microsoft.AspNetCore.Mvc;
using Product_Management_WebApp.Dto;

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

        public IActionResult CreateUser (UserDto dto)
        {

        }
    }
}
