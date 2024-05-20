using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eshop.Service.src.ServiceAbstraction;
using Eshop.Service.src.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eshop.WebAPI.src.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // POST: Create a new category
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryReadDTO>> CreateAsync([FromBody] CategoryCreateDTO categoryDTO)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryDTO);
            return Ok(createdCategory);
        }

        // GET: Get category by ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDTO>> GetByIdAsync(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        // PUT: Update a category by ID
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CategoryUpdateDTO categoryDTO)
        {
            await _categoryService.UpdateAsync(id, categoryDTO);
            return NoContent();
        }

        // DELETE: Delete a category by ID
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _categoryService.DeleteByIdAsync(id);
            return NoContent();
        }

        // GET: Get all categories
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: Find category by name
        [AllowAnonymous]
        [HttpGet("findByName")]
        public async Task<ActionResult<CategoryReadDTO>> FindByNameAsync([FromQuery] string name)
        {
            var category = await _categoryService.FindByNameAsync(name);
            return Ok(category);
        }
    }
}
