namespace Product_Management_WebApp.Dto
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;    
    }
}
