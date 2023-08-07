using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        
    }
}