using BookApp.Context;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class ProductService
    {

        private ApplicationDbContext _dbContext;
        public ProductService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ActionResult<Product>> AddProduct(Product product){
            var _product = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageURLs = product.ImageURLs,
                SoldAmount = product.SoldAmount,
                StockAmount = product.StockAmount,
                CategoryId = product.CategoryId
            };

           await  _dbContext.Products.AddAsync(_product);
          await  _dbContext.SaveChangesAsync();
          return product;
        }
        public void DeleteProduct(int Id)
        {
            var _product = _dbContext.Products.FirstOrDefault(product => product.Id == Id);
            if (_product != null)
            {
                _dbContext.Products.Remove(_product);

                _dbContext.SaveChanges();
            }
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
              
              return await _dbContext.Products.ToListAsync();
        
        }

        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            
            var result  =  await _dbContext.Products.FindAsync(Id);
            return result;
        }

      public async Task<ActionResult<Product?>> Update(Product product, int Id)
{
    var _product = await _dbContext.Products.FindAsync(Id);
    if (_product != null)
    {
        _product.Name = product.Name;
        _product.Description = product.Description;
        _product.Price = product.Price;
        _product.ImageURLs = product.ImageURLs;
        _product.SoldAmount = product.SoldAmount;
        _product.StockAmount = product.StockAmount;
        _product.CategoryId = product.CategoryId;

        _dbContext.Update(_product);
        await _dbContext.SaveChangesAsync();
    }
    return _product;
}

    }
   
}
