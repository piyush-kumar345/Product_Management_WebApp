using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;
using Product_Management_WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

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
                return View("Login");
            }

            else
            { 
                if(isUserExists.Password == dto.Password)   
                {
                     var tokan =   GenerateJwtTokan(dto);
                    Response.Cookies.Append("Jwt_Key", tokan, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });
                     return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Password";
                    return View("Login");
                }
            }           
        }

        private string GenerateJwtTokan(UserDto dto)
        {
            var jwtHnadler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("YehEoSYAdvxrutgGjPrDgA1ofiSEkC7n7Dor8IVJDp3");

            var tokanDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, dto.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)

            };
            var tokan = jwtHnadler.CreateToken(tokanDescriptor);

            return jwtHnadler.WriteToken(tokan);
        }

        public IActionResult LogoutUser()
        {
            Response.Cookies.Delete("Jwt_Key");
            return RedirectToAction("Login");
        }


    }

}
