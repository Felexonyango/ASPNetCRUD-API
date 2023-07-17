using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookApp.Errors;
using AutoMapper;
using BookApp.DTos;



namespace BookApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ProductService _productService;

        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }
        
 [HttpPost("create-product")]
public async Task<ActionResult<ProductDtos>> AddProduct(ProductDtos productDto)
{
    var product = _mapper.Map<Product>(productDto);
    
    await _productService.AddProduct(product);

    return Ok();
}



        [Authorize]
        [HttpGet("allproducts")]
        public async Task<ActionResult<IEnumerable<ProductDtos>>> GetAllProducts()
        {

            var allproducts = await _productService.GetAllProducts();
            var productDtos = _mapper.Map<IEnumerable<ProductDtos>>(allproducts);

            return Ok(productDtos);
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
