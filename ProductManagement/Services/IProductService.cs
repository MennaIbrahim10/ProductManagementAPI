using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        public IEnumerable<object> GetAllProducts();
        public IEnumerable<object> GetCategoryProducts(int CategoryId);
        public object GetProductById(int id);
        public bool AddnewProduct(Product product);
        public bool Update_Product(Product product, int id);
        public bool Delete_Product(int id);
    }
}
