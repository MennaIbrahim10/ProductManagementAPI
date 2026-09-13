using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<CategoryDto>> GetAll()
        {
            return Ok(_categoryService.GetAllCategories());
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<CategoryDto> GetById(int id)
        {
            CategoryDto result = _categoryService.GetCategoryById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Route("")]
        public ActionResult CreateCategory(CategoryRequestDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
            };
             CategoryDto result= _categoryService.CreateNewCategory(category);

            if (result != null)
            {
                return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
                );
            }
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateCategory(CategoryRequestDto categoryDto, int id)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
            };
            if (_categoryService.UpdateCategory(category, id)) 
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            DeleteCategoryResult result = _categoryService.DeleteCategory(id);

            if (result == DeleteCategoryResult.NotFound)
                return NotFound();
            else if (result == DeleteCategoryResult.HasProducts)
                return Conflict();
            else
                return NoContent();
        }
    }
}
