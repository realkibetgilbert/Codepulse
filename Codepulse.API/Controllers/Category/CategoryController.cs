using Codepulse.API.Application.DTOs.Category;
using Codepulse.API.Application.Features.Categ.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Codepulse.API.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CategoryToCreateDto categoryToCreateDto)
        {
            var category = await _categoryService.CreateAsync(categoryToCreateDto);
            return Ok(category);
        }
        [HttpGet]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetAll(
        [FromQuery] string? query,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortDirection,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
        {
            var categories = await _categoryService.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpPut]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] CategoryToUpdateDto categoryToUpdateDto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(id, categoryToUpdateDto);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var deletedCategory = await _categoryService.DeleteAsync(id);

            if (deletedCategory == null)
                return NotFound();

            return Ok(deletedCategory);
        }

        [HttpGet]
        [Route("count")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> GetCategoriesTotal()
        {
            var total = await _categoryService.CountAsync();
            return Ok(total);
        }

    }
}
