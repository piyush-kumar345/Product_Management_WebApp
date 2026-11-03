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
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                ViewBag.ErrorMessage = "Kindly Fill All The Details";
                return View("Register");
            }

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

        public async Task<IActionResult> LoginUser(UserDto dto)
        {
            var isUserExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (isUserExists == null)
            {
                ViewBag.ErrorMessage = "User Is Not Exist";
                return View();
            }

            else
            {
                if(isUserExists.Password== dto.Password)
                {
                    return RedirectToAction("Index", " Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Message";
                    return View("Login");
                }
            }
           
        }
    }
}
