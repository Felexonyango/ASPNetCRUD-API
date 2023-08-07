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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FileUpload> Files { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
    
}
