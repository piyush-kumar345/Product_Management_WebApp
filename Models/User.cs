using System.ComponentModel.DataAnnotations;

namespace Product_Management_WebApp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
