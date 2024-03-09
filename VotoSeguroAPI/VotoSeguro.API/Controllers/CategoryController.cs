using VotoSeguro.Domain.Enum;
using VotoSeguro.DTO.Base;
using VotoSeguro.Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VotoSeguro.API.Controllers
{
    public class CategoryController(ICategoryService categoryService) : BaseController
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory([FromBody] BasicDTO name)
        {
            var category = await _categoryService.Create(name);
            return StatusCode(category.Code, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] BasicDTO name)
        {
            var category = await _categoryService.Update(id, name);
            return StatusCode(category.Code, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory([FromRoute] Guid id)
        {
            var category = await _categoryService.Remove(id);
            return StatusCode(category.Code, category);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCategories()
        {
            var category = await _categoryService.GetList();
            return StatusCode(category.Code, category);
        }
    }
}