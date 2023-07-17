using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookApp.Errors;


namespace BookApp.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {

            await _productService.AddProduct(product);

            return Ok();
        }
        [Authorize]
        [HttpGet("allproducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var allproducts =  await _productService.GetAllProducts();

            return Ok(allproducts);
        }
        [HttpGet("getProduct/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null) return NotFound(new ApiResponse(404));
            var responseBody = new
            {
                message = "Successfully retrieved product",
                result = product
            };

            return Ok(responseBody);

        }

        [HttpDelete("delete/{id}")]

        public IActionResult DeletProductById(int id)
        {

            _productService.DeleteProduct(id);
            return Ok();
        }
        [HttpPut("update-product/{id}")]
        public async Task<ActionResult<Product>> UpdateProductById([FromBody] Product product, int id)
        {

            if (product == null) return NotFound(new ApiResponse(404));
             await _productService.Update(product, id);
            return Ok();
        }




    }
}
