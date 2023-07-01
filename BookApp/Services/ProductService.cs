using BookApp.Context;
using BookApp.Models;
namespace BookApp.Services
{
    public class ProductService
    {

        private ApplicationDbContext _dbContext;
        public ProductService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void AddProduct(Product product){
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

            _dbContext.Products.Add(_product);
            _dbContext.SaveChanges();
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

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(int Id)
        {
            return _dbContext.Products.FirstOrDefault(product => product.Id == Id);
        }

        public void Update(Product product, int Id)
        {
            var _product = _dbContext.Products.FirstOrDefault(product => product.Id == Id);
            if (_product != null)
            {
                _product.Name = product.Name;
                _product.Description = product.Description;
                _product.Price = product.Price;
                _product.ImageURLs = product.ImageURLs;
                _product.SoldAmount = product.SoldAmount;
                _product.StockAmount = product.StockAmount;
                _product.CategoryId = product.CategoryId;

                _dbContext.Products.Add(_product);
                _dbContext.SaveChanges();
            }
        }
    }
   
}
