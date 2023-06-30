using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Authour { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }


    }
}
