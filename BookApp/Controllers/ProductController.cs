using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;

        }
        [HttpPost("create-product")]
        public IActionResult AddProduct(Product product)
        {
           /* var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(jsonText);*/
            _productService.AddProduct(product);

            return Ok();
        }

    }
}
