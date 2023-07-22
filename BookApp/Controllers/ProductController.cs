using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookApp.Errors;
using BookApp.DTos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;

namespace BookApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductService _productService;
        public UserService _userService;

        public ProductController(
            ProductService productService,
            UserService userService
        )
        {
            _productService = productService;
            _userService = userService;
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ProductDtos>> AddProduct(ProductDtos productDto)
        {
            var currentUser = await _userService.GetCurrentUser();

            if (currentUser == null)
            {
                return Unauthorized(new ApiResponse(401, "User not authenticated."));
            }

            int currentUserId = currentUser.Id;

            var product = productDto.Adapt<Product>(); // Use Mapster for mapping

            await _productService.AddProduct(product, currentUserId);

            return Ok();
        }

        [HttpGet("allproducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProducts();
            return Ok(allProducts);
        }

        [HttpGet("getProduct/{id}")]
        public async Task<ActionResult<ProductDtos>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            var productDto = product.Adapt<ProductDtos>(); // Use Mapster for mapping

            if (product == null)
                return NotFound(new ApiResponse(404));

            var responseBody = new
            {
                message = "Successfully retrieved product",
                result = productDto
            };

            return Ok(responseBody);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProductById(int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }

        [HttpPut("update-product/{id}")]
        public async Task<ActionResult<Product>> UpdateProductById([FromBody] ProductDtos productDto, int id)
        {
            var product = productDto.Adapt<Product>(); // Use Mapster for mapping

            if (product == null)
                return NotFound(new ApiResponse(404));

            await _productService.Update(product, id);

            return Ok();
        }
    }
}
