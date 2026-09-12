using ProductManagement.Data;
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

        public IEnumerable<object> GetAllCategories()
        {
            var records = _context.categories.Select(c => new { c.Id, c.Name, c.Description, products = c.products.Select(p => new { p.Id, p.Name }) }).ToList();
            return records;
        }
        public object GetCategoryById(int id)
        {
            var record = _context.categories.Where(c => c.Id == id).Select(c => new { c.Id, c.Name, c.Description, products = c.products.Select(p => new { p.Id, p.Name }) }).FirstOrDefault();
            return record;
        }
        public bool CreatenewCategory(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            return true;
        }

        public bool Update_Category(Category category, int id)
        {
            var existingCategory = _context.categories.Find(id);
            if (existingCategory == null)
                return false;
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            _context.SaveChanges();
            return true;
        }
        public int Delete_Category(int id)
        {
            var ExistingCategory = _context.categories.Find(id);
            if (ExistingCategory == null)
                return 1;
            if (_context.products.Any(p => p.CategoryId == id))
                return 2;
            _context.categories.Remove(ExistingCategory);
            _context.SaveChanges();
            return 3;
        }
    }
}
