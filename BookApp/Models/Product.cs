using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockAmount { get; set; }
        public int SoldAmount { get; set; }
        public string ImageURLs { get; set; }
        public int CategoryId { get; set; }
        

        public int UserId { get; set; }
        
        // Navigation property for the User who created this product
        public User User { get; set; }
    }
}
