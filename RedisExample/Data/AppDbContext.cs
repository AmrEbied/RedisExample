using Microsoft.EntityFrameworkCore;
using RedisExample.Model;

namespace RedisExample.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {
        }
       
    }
}
