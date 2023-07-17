using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.DTos
{
    public class BookDto
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