using Eshop.Core.src.Common;
using Eshop.Service.src.DTO;
using Eshop.Service.src.ServiceAbstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Eshop.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/product-lines")]
    public class ProductLineController : ControllerBase
    {
        private readonly IProductLineService _productLineService;

        public ProductLineController(IProductLineService productLineService)
        {
            _productLineService = productLineService;
        }

        // POST: Create a new product line
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductLineReadDTO>> CreateAsync([FromBody] ProductLineCreateDTO productLineDto)
        {
            var createdProductLine = await _productLineService.CreateAsync(productLineDto);
            return Ok(createdProductLine);
        }

        // GET: Get product line by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductLineReadDTO>> GetByIdAsync(Guid id)
        {
            var productLine = await _productLineService.GetByIdAsync(id);
            if (productLine == null)
                return NotFound();

            return Ok(productLine);
        }

        // PUT: Update a product line
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductLineUpdateDTO productLineDto)
        {
            var updateResult = await _productLineService.UpdateAsync(id, productLineDto);
            if (!updateResult)
                return NotFound();

            return NoContent();
        }

        // DELETE: Delete a product line
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleteResult = await _productLineService.DeleteByIdAsync(id);
            if (!deleteResult)
                return NotFound();

            return NoContent();
        }

        // GET: Get all product lines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductLineReadDTO>>> GetAllProductLinesAsync([FromQuery] QueryOptions options)
        {
            var productLines = await _productLineService.GetAllProductLinesAsync(options);
            return Ok(productLines);
        }
    }
}
