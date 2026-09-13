using ProductManagement.Data;
using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<ProductDto> GetAllProducts ()
        {
            var records = _context.products.Select(
                p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock,
                    Category = new CategorySummaryDto
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name
                    }
                });
            return records;
        }

        public IEnumerable<ProductDto> GetCategoryProducts(int categoryId)
        {
            var records = _context.products
            .Where(p => p.CategoryId == categoryId)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Category = new CategorySummaryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            });
            return records;
        }

        public ProductDto GetProductById (int id)
        {
            var record = _context.products.Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                Category = new CategorySummaryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                }
            })
           .FirstOrDefault();
            return record;
        }

        public ProductDto AddNewProduct(Product product)
        {
            if (!_context.categories.Any(c => c.Id == product.CategoryId))
                return null;

            _context.products.Add(product);
            _context.SaveChanges();

            var category = _context.categories.Find(product.CategoryId);
            ProductDto productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Category = new CategorySummaryDto
                {
                    Id = category.Id,
                    Name = category.Name
                }
            };      
            return productDto;
        }

        public bool UpdateProduct(Product product, int id)
        {
            var existingProduct = _context.products.Find(id);
            if (existingProduct == null || !(_context.categories.Any(c => c.Id == product.CategoryId)))
                return false;
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var existingProduct = _context.products.Find(id);
            if (existingProduct == null)
                return false;
            _context.products.Remove(existingProduct);
            _context.SaveChanges();
            return true;
        }

    }
}
