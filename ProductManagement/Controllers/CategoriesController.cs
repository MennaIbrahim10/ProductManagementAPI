using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
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
        public ActionResult<IEnumerable<object>> GetAll()
        {
            return Ok(_categoryService.GetAllCategories());
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<object> GetById(int id)
        {
            object Result = _categoryService.GetCategoryById(id);
            return Result == null ? NotFound() : Ok(Result);
        }

        [HttpPost]
        [Route("")]
        public ActionResult CreateCategory(Category category)
        {
            if (_categoryService.CreatenewCategory(category))
            {
                return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category
                );
            }
            else
                return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateCategory(Category category, int id)
        {
            if(_categoryService.Update_Category(category, id)) 
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            int result = _categoryService.Delete_Category(id);
            if (result == 1)
                return NotFound();
            else if (result == 2)
                return StatusCode(403);
            else
                return NoContent();
        }
    }
}
