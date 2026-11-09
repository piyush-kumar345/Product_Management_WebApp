using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;
using System.Drawing;

namespace Product_Management_WebApp.Controllers
{
    public class DashboardController(AppDbConetxt context) : Controller
    {

        public IActionResult Index()

        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var list = context.Products.Select(x => new ProductDto { Id = x.Id, ProductName= x.ProductName,Description = x.Description,Price= x.Price,Color= x.Color  }).ToList();
            return View(list);
        }

        public IActionResult ProductForm() => View();

        public IActionResult UpdateProductForm(int id)
        {
            var data = context.Products.Select(x => new ProductDto { Id = x.Id, ProductName = x.ProductName, Description = x.Description, Price = x.Price, Color = x.Color }).FirstOrDefault(x => x.Id == id);
            return View(data); 
        }

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

        public async Task<IActionResult> UpdateProduct(ProductDto dto)
        {
            if(dto == null)
            {
                ViewBag.ErrorMessage = "Please Fill all the details";
                return View("UpdateProductForm");
            }
            var data = context.Products.FirstOrDefault(x => x.Id == dto.Id);

            data.ProductName = dto.ProductName;
            data.Description = dto.Description;
            data.Price = dto.Price;
            data.Color = dto.Color;

            context.Products.Update(data);
            await context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Product Updated";


            return RedirectToAction("Index");
        }

            
    }
}
