using BookApp.Models;
using Microsoft.EntityFrameworkCore;
namespace BookApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User>Users { get; set; }
    }
    
}
