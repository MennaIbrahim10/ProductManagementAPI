using ProductManagement.Data;
using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var records = _context.categories.Select(c => new CategoryDto
            { 
                Id = c.Id, 
                Name = c.Name,
                Description = c.Description, 
                Products =  c.Products.Select(p => new ProductSummaryDto 
                { 
                    Id = p.Id, 
                    Name = p.Name 
                }) 
            }).ToList();
            return records;
        }
        public CategoryDto GetCategoryById(int id)
        {
            var record = _context.categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Products = c.Products.Select(p => new ProductSummaryDto
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                })
                .FirstOrDefault();

            return record;
        }
        public CategoryDto CreateNewCategory(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            CategoryDto categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
            return categoryDto ;
        }

        public bool UpdateCategory(Category category, int id)
        {
            var existingCategory = _context.categories.Find(id);
            if (existingCategory == null)
                return false;
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            _context.SaveChanges();
            return true;
        }
        public DeleteCategoryResult DeleteCategory(int id)
        {
            var existingCategory = _context.categories.Find(id);

            if (existingCategory == null)
                return DeleteCategoryResult.NotFound;

            if (_context.products.Any(p => p.CategoryId == id))
                return DeleteCategoryResult.HasProducts;

            _context.categories.Remove(existingCategory);
            _context.SaveChanges();

            return DeleteCategoryResult.Deleted;
        }
    }
}
