using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductDto> GetAllProducts();
        public IEnumerable<ProductDto> GetCategoryProducts(int categoryId);
        public ProductDto GetProductById(int id);
        public ProductDto AddNewProduct(Product product);
        public bool UpdateProduct(Product product, int id);
        public bool DeleteProduct(int id);
    }
}
