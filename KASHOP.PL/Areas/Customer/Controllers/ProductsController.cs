using KASHOP.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Customer")]
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

        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetByCategory(Request, categoryId, page, pageSize);
            return Ok(products);
        }

        
        [HttpGet("{id:int}/reviews")]
        public async Task<IActionResult> GetReviews(int id)
        {
            var reviews = await _productService.GetReviews(id);
            return Ok(reviews);
        }
    }
}
