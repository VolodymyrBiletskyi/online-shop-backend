using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Contracts.Category;
using api.Dto;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories =  await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategory createCategory)
        {
            var created = await _categoryService.CreateAsync(createCategory);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CategoryDto>> Update( Guid id, [FromBody] UpdateCategory updateCategory)
        {
            var update = await _categoryService.UpdateAsync(id, updateCategory);
            return Ok(update);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound("Category not found");
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}