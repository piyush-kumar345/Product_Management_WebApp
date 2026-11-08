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
            var list = context.Products.Select(x => new ProductDto { Id = x.Id, ProductName= x.ProductName,Description = x.Description,Price= x.Price,Color= x.Color  }).ToList();
            return View(list);
        }

        public IActionResult ProductForm() => View();

        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {
            if(dto== null)
            {
                ViewBag.ErrorMessage = "Please fill the all details!";
                return View("ProductForm");
            }
            context.Products.Add(new Models.Product
            {
               ProductName = dto.ProductName,
               Description = dto.Description,
               Price = dto.Price,
               Color = dto.Color
            });
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(int productid)
        {
            var product =await context.Products.FirstOrDefaultAsync(x=> x.Id == productid);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

            
    }
}
