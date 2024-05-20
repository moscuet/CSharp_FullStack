using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;

namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/product-colors")]
    public class ProductColorController : ControllerBase
    {
        private readonly IProductColorService _productColorService;

        public ProductColorController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        // POST: Create a new product color
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductColorReadDTO>> CreateAsync([FromBody] ProductColorCreateDTO productColorDto)
        {
            var createdProductColor = await _productColorService.CreateAsync(productColorDto);
            return Ok(createdProductColor);
        }

        // GET: Get product color by ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColorReadDTO>> GetByIdAsync(Guid id)
        {
            var productColor = await _productColorService.GetByIdAsync(id);
            if (productColor == null)
                return NotFound();

            return Ok(productColor);
        }

        // GET: Get all product colors
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColorReadDTO>>> GetAllAsync()
        {
            var productColors = await _productColorService.GetAllAsync();
            return Ok(productColors);
        }

        // PUT: Update a product color
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductColorUpdateDTO productColorDto)
        {
            var updateResult = await _productColorService.UpdateAsync(id, productColorDto);
            if (!updateResult)
                return NotFound();

            return NoContent();
        }

        // DELETE: Delete a product color
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteResult = await _productColorService.DeleteByIdAsync(id);
            if (!deleteResult)
                return NotFound();

            return NoContent();
        }
    }
}
