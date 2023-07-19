using API.Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Fullstack.Data
{
    public class FullstackDbContext : DbContext
    {
        public FullstackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
