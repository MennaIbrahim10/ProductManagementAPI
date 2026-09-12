using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
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

        public IEnumerable<object> GetAllProducts ()
        {
            var records = _context.products.Select(
                p => new {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.Stock,
                    Category = new { p.category.Id, p.category.Name }
                }).ToList();
            return records;
        }

        public IEnumerable<object> GetCategoryProducts(int CategoryId)
        {
            var records = _context.products
            .Where(p => p.category.Id == CategoryId)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                Category = new{p.category.Id, p.category.Name}
            }).ToList();
            return records;
        }

        public object GetProductById (int id)
        {
            var record = _context.products.Where(p => p.Id == id)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                Category = new
                {
                    p.category.Id,
                    p.category.Name
                }
            })
           .FirstOrDefault();
            return record;
        }

        public bool AddnewProduct (Product product)
        {
            if (!_context.categories.Any(c => c.Id == product.CategoryId))
                return false;
            _context.products.Add(product);
            _context.SaveChanges();
            return true;
        }

        public bool Update_Product (Product product, int id)
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

        public bool Delete_Product (int id)
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
