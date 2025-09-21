using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KASHOP.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("")]
        public IActionResult AddToCart(CartRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _cartService.AddToCart(request,userId);
            return result ? Ok() : BadRequest();
        }
        [HttpGet("")]

        public IActionResult GetUerCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _cartService.CartSummaryResponse(userId);
            return Ok(result);
        }

    }
}