using Microsoft.EntityFrameworkCore;
using FirstProject.Api.Models;
namespace FirstProject.Api.Data
{
    public class Appdbcontext : DbContext
    {
        public Appdbcontext(DbContextOptions<Appdbcontext> options)
          : base(options)
            {
            }
        public DbSet<Product> Products { get; set; }
    }
}
