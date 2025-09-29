using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CategoryRequest request)
        {
            var id =await _categoryService.CreateCategory(request);
            return Ok(new { message = "Category created successfully", categoryId = id });
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = await _categoryService.UpdateCategory(id, request);
            return updated ? Ok(new { message = "Category updated successfully" }) : 
                NotFound(new { message = "Category not found" });
        }
        [HttpPatch("{id}/toggleStatus")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id)
        {
            var updated = await _categoryService.ToggleStatus(id);
            if(updated)
                return Ok(new { message = "Category status toggled successfully" });
            else
                return NotFound(new { message = "Category not found" });
        }
        [HttpDelete("")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteCategory(id);
            return deleted ? Ok(new { message = "Category deleted successfully" }) : 
                NotFound(new { message = "Category not found" });
        }
    }
}
