using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;

namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/product-sizes")]
    public class ProductSizeController : ControllerBase
    {
        private readonly IProductSizeService _productSizeService;

        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        // POST: Create a new product size
        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductSizeReadDTO>> CreateAsync([FromBody] ProductSizeCreateDTO productSizeDto)
        {
            var createdProductSize = await _productSizeService.CreateAsync(productSizeDto);
            return Ok(createdProductSize);
        }

        // GET: Get product size by ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSizeReadDTO>> GetByIdAsync(Guid id)
        {
            var productSize = await _productSizeService.GetByIdAsync(id);
            if (productSize == null)
                return NotFound();

            return Ok(productSize);
        }

        // GET: Get all product sizes
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSizeReadDTO>>> GetAllAsync()
        {
            var productSizes = await _productSizeService.GetAllAsync();
            return Ok(productSizes);
        }

        // PUT: Update a product size
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductSizeUpdateDTO productSizeDto)
        {
            var updateResult = await _productSizeService.UpdateAsync(id, productSizeDto);
            if (!updateResult)
                return NotFound();

            return NoContent();
        }

        // DELETE: Delete a product size
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteResult = await _productSizeService.DeleteByIdAsync(id);
            if (!deleteResult)
                return NotFound();

            return NoContent();
        }
    }
}
