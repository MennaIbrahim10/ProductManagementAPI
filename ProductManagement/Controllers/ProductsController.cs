using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Authorization;
using ProductManagement.DTOs;
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
        public ActionResult<IEnumerable<ProductDto>> GetAll([FromQuery] int ? categoryId)
        {
            if (categoryId.HasValue)
                return Ok(_productService.GetCategoryProducts(categoryId.Value));

            return Ok(_productService.GetAllProducts());
        }

        [HttpGet]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.ReadProducts)]
        public ActionResult<ProductDto> GetById([FromRoute]int id)
        {
            _logger.LogInformation("Getting product with id {id}", id);

            ProductDto result = _productService.GetProductById(id);
            if (result == null)
            {
                _logger.LogWarning("Product with id {id} was not found", id);
                return NotFound();  
            }

            return Ok(result);
        }



        [HttpPost]
        [Route("")]
        [PermissionBasedAuthorization(Permission.AddProducts)]
        public ActionResult AddProduct([FromBody] ProductRequestDto productDto)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };

            ProductDto result = _productService.AddNewProduct(product);

            if (result != null)
                return CreatedAtAction(nameof(GetById), new { id = result.Id },result);
            else
                return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.EditProducts)]
        public ActionResult UpdateProduct(ProductRequestDto productDto, int id)
        {
            Product product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };
            
            if (_productService.UpdateProduct(product, id)) 
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [PermissionBasedAuthorization(Permission.DeleteProducts)]
        public ActionResult DeleteProduct(int id)
        {
            
            if (_productService.DeleteProduct(id))
                return NoContent();
            else
                return NotFound();          
        }
    }
}
