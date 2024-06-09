using System.Text.Json;
using Eshop.Core.src.Common;
using Eshop.Core.src.Entity;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // POST: Create a new product
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync([FromBody] ProductCreateDTO productDto)
        {
            var createdProduct = await _productService.ProductCreateAsync(productDto);
            return Ok(createdProduct);
        }

        // GET: Get product by ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDTO>> GetByIdAsync(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // GET: Get all products
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAllProductsAsync([FromQuery] QueryOptions? options)
        {
            var products = await _productService.GetAllProductsAsync(options);
            return Ok(products);
        }

        // PUT: Update a product
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
       
        public async  Task<ActionResult<Product>> UpdateAsync(Guid id, [FromBody] ProductUpdateDTO productDto)
        {
            var updateResult = await _productService.UpdateProductWithImagesAsync(id, productDto);
            return updateResult;
        }

        // DELETE: Delete a product
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteResult = await _productService.DeleteByIdAsync(id);
            if (!deleteResult)
                return NotFound();

            return NoContent();
        }
    }
}
