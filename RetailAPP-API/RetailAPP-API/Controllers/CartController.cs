using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAPP_API.DTOs.CartDTOs;
using RetailAPP_API.Services;

namespace RetailAPP_API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize] // Assuming Identity is handled
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private string CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "guest_user";
        // Note: 'guest_user' is for testing if [Authorize] is disabled

        // GET: /api/cart
        [HttpGet]
        public async Task<ActionResult<CartSummaryDto>> GetCart()
        {
            var cart = await _cartService.GetCartSummaryAsync(CurrentUserId);
            return Ok(cart);
        }

        // POST: /api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto addDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _cartService.AddToCartAsync(CurrentUserId, addDto);
            if (!success) return BadRequest(new { message = "Could not add item to cart. Check stock availability." });

            return Ok(new { message = "Item added to cart successfully." });
        }

        // DELETE: /api/cart/remove/{itemId}
        [HttpDelete("remove/{itemId}")]
        public async Task<IActionResult> RemoveFromCart(int itemId)
        {
            var success = await _cartService.RemoveFromCartAsync(CurrentUserId, itemId);
            if (!success) return NotFound(new { message = "Cart item not found." });

            return NoContent();
        }

        // POST: /api/cart/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                var orderId = await _cartService.CheckoutAsync(CurrentUserId);
                if (orderId == null) return BadRequest(new { message = "Cart is empty." });

                return Ok(new { message = "Order placed successfully.", orderId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}