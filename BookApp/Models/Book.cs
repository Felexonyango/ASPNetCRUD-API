using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        
        // Foreign key referencing the User who created this book
        public int UserId { get; set; }
    // Navigation property for the User who created this product
        public User User { get; set; }

    }
}
