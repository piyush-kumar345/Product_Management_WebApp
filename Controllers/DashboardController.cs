using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;
using System.Drawing;

namespace Product_Management_WebApp.Controllers
{
    [Authorize]
    public class DashboardController(AppDbConetxt context) : Controller
    {

        public IActionResult Index()

        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            var list = context.Products.Select(x => new ProductDto { Id = x.Id, ProductName= x.ProductName,Description = x.Description,Price= x.Price,Color= x.Color  }).ToList();
            return View(list);
        }

        public IActionResult ProductForm() => View();

        [HttpGet]
        public async Task<IActionResult> UpdateProductForm(int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return View(new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Color = product.Color
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("ProductForm", dto);
            }
[HttpPost]
[ValidateAntiForgeryToken]
            public async Task<IActionResult> UpdateProduct(ProductDto dto)
{
    if (!ModelState.IsValid)
    {
        return View("UpdateProductForm", dto);
    }

    var product = await context.Products
        .FirstOrDefaultAsync(product => product.Id == dto.Id);

    if (product is null)
    {
        return NotFound();
    }

    product.ProductName = dto.ProductName.Trim();
    product.Description = dto.Description.Trim();
    product.Price = dto.Price;
    product.Color = dto.Color.Trim();

    await context.SaveChangesAsync();

    TempData["SuccessMessage"] = "Product updated successfully.";

    return RedirectToAction(nameof(Index));
}
            context.Products.Add(new Models.Product
            {
                ProductName = dto.ProductName.Trim(),
                Description = dto.Description.Trim(),
                Price = dto.Price,
                Color = dto.Color.Trim()
            });

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



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
            var data = context.Products.FirstOrDefault(x => x.Id == dto.Id );

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
