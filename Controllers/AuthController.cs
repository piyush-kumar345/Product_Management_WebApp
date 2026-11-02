using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;
using Product_Management_WebApp.Models;

namespace Product_Management_WebApp.Controllers
{
    public class AuthController(AppDbConetxt _context) : Controller
    {
        public IActionResult Register()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"]; 
            return View();  
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser (UserDto dto)
        
            {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

              if(existingUser == null)
            {
                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                ViewBag.ErrorMessage = "Email Already Exist";
                return View("Register");
            }
            TempData["SuccessMessage"] = "User Created Successfully.Please Login";
            return RedirectToAction("Login");
               
        }
    }
}
