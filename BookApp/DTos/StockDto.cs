using BookApp.Models;

namespace BookApp.DTos
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        
    }
}