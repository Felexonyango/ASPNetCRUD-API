using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookApp.Errors;
using AutoMapper;
using BookApp.DTos;
using Microsoft.AspNetCore.Http;




namespace BookApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ProductService _productService;
        public UserService _userService;

        public ProductController(
            ProductService productService,
         IMapper mapper,
         UserService userService
         )
        {
            _productService = productService;
            _mapper = mapper;
            _userService = userService;

        }


  [HttpPost("create-product")]
public async Task<ActionResult<ProductDtos>> AddProduct(ProductDtos productDto)
{
    var product = _mapper.Map<Product>(productDto);
    var currentUser = await _userService.GetCurrentUser();

    if (currentUser == null)
    {
        return Unauthorized(new ApiResponse(401, "User not authenticated."));
    }

    int currentUserId = currentUser.Id;

    await _productService.AddProduct(product, currentUserId);

    return Ok();
}




     
        [HttpGet("allproducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {

            var allproducts = await _productService.GetAllProducts();
            
            return Ok(allproducts);
        }
        [HttpGet("getProduct/{id}")]
        public async Task<ActionResult<ProductDtos>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            var productdto = _mapper.Map<ProductDtos>(product);
            if (product == null) return NotFound(new ApiResponse(404));
            var responseBody = new
            {
                message = "Successfully retrieved product",
                result = productdto
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
