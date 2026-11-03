using Microsoft.AspNetCore.Mvc;

namespace Product_Management_WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
