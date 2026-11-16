using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Product_Management_WebApp.Data;
using Product_Management_WebApp.Dto;
using Product_Management_WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product_Management_WebApp.Controllers
{
    public class AuthController(
        AppDbConetxt context,
        IPasswordHasher<User> passwordHasher) : Controller
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

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                ViewBag.ErrorMessage = "Kindly fill in all the details.";
                return View("Register", dto);
            }

            var existingUser = await context.Users
                .FirstOrDefaultAsync(user => user.Email == dto.Email);

            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Email already exists.";
                return View("Register", dto);
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email
            };

            user.Password = passwordHasher.HashPassword(user, dto.Password);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User created successfully. Please log in.";
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserDto dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                ViewBag.ErrorMessage = "Please enter your email and password.";
                return View("Login", dto);
            }

            var user = await context.Users
                .FirstOrDefaultAsync(item => item.Email == dto.Email);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Login", dto);
            }

            var verificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.Password);

            if (verificationResult != PasswordVerificationResult.Success &&
                verificationResult != PasswordVerificationResult.SuccessRehashNeeded)
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View("Login", dto);
            }

            var token = GenerateJwtToken(user.Email);

            Response.Cookies.Append("Jwt_Key", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return RedirectToAction("Index", "Dashboard");
        }

        private string GenerateJwtToken(string email)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(
                "YehEoSYAdvxrutgGjPrDgA1ofiSEkC7n7Dor8IVJDp3");

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, email)
                ]),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }

        public IActionResult LogoutUser()
        {
            Response.Cookies.Delete("Jwt_Key");
            return RedirectToAction(nameof(Login));
        }
    }
}