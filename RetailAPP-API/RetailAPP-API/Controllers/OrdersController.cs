using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAPP_API.DTOs.OrderDTOs;
using RetailAPP_API.Services;
using System.Security.Claims;

namespace RetailAPP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication by default
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private string GetCurrentUserId()
        {

            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
        {
            var userId = GetCurrentUserId();
            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var userId = GetCurrentUserId();
            var order = await _orderService.GetOrderByIdAsync(id, userId);

            if (order == null)
                return NotFound(new { Message = "Order not found or access denied." });

            return Ok(order);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _orderService.UpdateOrderStatusAsync(id, model.Status);

            if (!success)
                return NotFound(new { Message = "Order not found." });

            return Ok(new { Message = "Order status updated successfully." });
        }
    }
}
