using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAPP_API.DTOs.OrderDTOs;
using RetailAPP_API.Services;
using System.Security.Claims;

namespace RetailAPP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] PlaceOrderDto placeOrderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    Message = "Invalid request data",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });

            if (string.IsNullOrWhiteSpace(placeOrderDto.CustomerName))
                return BadRequest(new { Message = "Customer name is required" });

            if (string.IsNullOrWhiteSpace(placeOrderDto.CustomerPhone))
                return BadRequest(new { Message = "Customer phone is required" });

            if (string.IsNullOrWhiteSpace(placeOrderDto.CustomerAddress))
                return BadRequest(new { Message = "Customer address is required" });

            try
            {
                var userId = GetCurrentUserId();
                var createdOrder = await _orderService.CreateOrderAsync(userId, placeOrderDto);

                if (createdOrder == null)
                    return BadRequest(new
                    {
                        Message = "Order creation failed. Please ensure you have items in your cart with sufficient stock."
                    });

                return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while creating the order",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
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

