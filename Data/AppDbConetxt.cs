using Microsoft.EntityFrameworkCore;
using Product_Management_WebApp.Models;

namespace Product_Management_WebApp.Data
{
    public class AppDbConetxt(DbContextOptions<AppDbConetxt>options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
