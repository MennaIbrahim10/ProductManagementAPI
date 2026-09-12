using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Authorization;
using ProductManagement.Models;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger ;
        }


        [HttpGet]
        [Route("")]
        [PermissionBasedAuthorization(Permission.ReadProducts)]
        public ActionResult<IEnumerable<object>> GetAll([FromQuery] int ? categoryId)
        {
            if (categoryId.HasValue)
                return Ok(_productService.GetCategoryProducts(categoryId.Value));

            return Ok(_productService.GetAllProducts());
        }

        [HttpGet]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.ReadProducts)]
        public ActionResult<object> GetById([FromRoute]int id)
        {
            _logger.LogInformation("Getting product with id {id}", id);

            object Result = _productService.GetProductById(id);
            if (Result == null)
            {
                _logger.LogWarning("Product with id {id} was not found", id);
                return NotFound();  
            }

            return Ok(Result);
        }



        [HttpPost]
        [Route("")]
        [PermissionBasedAuthorization(Permission.AddProducts)]
        public ActionResult AddProduct([FromBody] Product product)
        {
            if (_productService.AddnewProduct(product))
                return Ok(product);
            else 
                return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.EditProducts)]
        public ActionResult UpdateProduct(Product product, int id)
        {
            
            if (_productService.Update_Product(product, id)) 
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.DeleteProducts)]
        public ActionResult DeleteProduct(int id)
        {
            
            if (_productService.Delete_Product(id))
                return NoContent();
            else
                return NotFound();          
        }
    }
}
