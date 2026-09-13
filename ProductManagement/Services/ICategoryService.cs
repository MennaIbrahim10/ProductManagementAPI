using ProductManagement.DTOs;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        public CategoryDto CreateNewCategory(Category category);
        public bool UpdateCategory(Category category, int id);
        public DeleteCategoryResult DeleteCategory(int id);
    }
}
