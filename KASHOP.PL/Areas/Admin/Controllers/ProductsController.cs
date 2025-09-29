using KASHOP.BLL.Services.Classes;
using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] bool onlyActive = false)
        {
            var products = await _productService.GetAllProducts(Request, page, pageSize, onlyActive);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(Request, id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _productService.CreateProduct(request);
            return Ok(new { message = "Product created successfully", product = result });
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductRequest request)
        {
            var existingProduct = await _productService.GetById(Request, id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            var updatedProduct = _productService.Update(id, request);
            return Ok(new { message = "Product updated successfully", product = updatedProduct });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productService.GetById(Request, id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            _productService.Delete(id);
            return Ok(new { message = "Product deleted successfully" });
        }

        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var existingProduct = await _productService.GetById(Request, id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }
            var updatedProduct = _productService.ToggleStatus(id);
            return Ok(new { message = "Product status toggled successfully", product = updatedProduct });
        }
    }
}
