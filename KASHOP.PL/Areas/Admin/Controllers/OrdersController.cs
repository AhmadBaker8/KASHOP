using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("status/{orderStatus}")]
        public async Task<IActionResult> GetOrderByStatus(OrderStatus orderStatus)
        {
            var orders = await _orderService.GetByStatusAsync(orderStatus);
            return Ok(orders);
        }


        [HttpPatch("update-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, [FromBody] OrderStatus status)
        {
            var result = await _orderService.ChangeStatusAsync(orderId,status);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { Message = "Order status updated successfully" });
        }

        [HttpGet("user-orders/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId([FromRoute] string userId)
        {
            var orders = await _orderService.GetOrderByUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("user-by-order/{orderId}")]
        public async Task<IActionResult> GetUserByOrderId([FromRoute] int orderId)
        {
            var order = await _orderService.GetUserByOrderAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.AddAsync(order);
            if (createdOrder == null)
            {
                return BadRequest("Could not create order");
            }
            return Ok(createdOrder);
        }
        */
    }
}