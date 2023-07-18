using AutoMapper;
using BookApp.Context;
using BookApp.DTos;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Services
{
    public class ProductService
    {
         private readonly IMapper _mapper;
        private ApplicationDbContext _dbContext;
        public ProductService(ApplicationDbContext context,IMapper mapper)
        {
            _dbContext = context;
             _mapper = mapper;
        }
public async Task<Product> AddProduct(Product product,int userId)
{
   
     product.UserId = userId;
    await _dbContext.Products.AddAsync(product);
    await _dbContext.SaveChangesAsync();

    return product;
}

public async Task<IEnumerable<Product>> GetAllProducts()
{
    var products = await _dbContext.Products.ToListAsync();
  
    return products;
}


public async Task<ProductDtos> GetProductById(int Id)
{
    var product = await _dbContext.Products.FindAsync(Id);
    var ProductDtos = _mapper.Map<ProductDtos>(product);

    return ProductDtos;
}

public async Task<ProductDtos> Update(ProductDtos productDto, int Id)
{
    var product = await _dbContext.Products.FindAsync(Id);

  
     _mapper.Map(productDto, product);

    _dbContext.Update(product);
    await _dbContext.SaveChangesAsync();

    return productDto;
}

public void DeleteProduct(int Id)
{
    var product = _dbContext.Products.FirstOrDefault(product => product.Id == Id);

    if (product != null)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
}


      public async Task<Product?> Update(Product product, int Id)
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
