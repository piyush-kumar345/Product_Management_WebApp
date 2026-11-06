using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;

namespace Product_Management_WebApp.Controllers
{
    public class DashboardController(AppDbConetxt context) : Controller
    {

        public IActionResult Index()
        {
            var list = context.Products.Select(x => new ProductDto { Id = x.Id, color = x.color
                , Price = x.Price, Description = x.Description }).ToList();
            return View(list);
        }

        public IActionResult ProductForm() => View();

            
    }
}
