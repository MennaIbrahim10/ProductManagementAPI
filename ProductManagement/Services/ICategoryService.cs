using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface ICategoryService
    {
        public IEnumerable<object> GetAllCategories();
        public object GetCategoryById(int id);
        public bool CreatenewCategory(Category category);
        public bool Update_Category(Category category, int id);
        public int Delete_Category(int id);
    }
}
