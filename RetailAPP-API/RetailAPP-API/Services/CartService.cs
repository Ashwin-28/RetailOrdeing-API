using Microsoft.EntityFrameworkCore;
using RetailAPP_API.Data;
using RetailAPP_API.DTOs.CartDTOs;
using RetailAPP_API.Models;
using RetailAPP_API.Models.Enums;

namespace RetailAPP_API.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private const decimal TaxRate = 0.18m; // 18% GST

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartSummaryDto> GetCartSummaryAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);

            var itemsDto = cart.CartItems.Select(ci => new CartItemResponseDto
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                ProductImageUrl = ci.Product.ImageUrl,
                UnitPrice = ci.Product.Price,
                Quantity = ci.Quantity,
                LineTotal = ci.Product.Price * ci.Quantity,
                AvailableStock = ci.Product.StockQuantity
            }).ToList();

            var subTotal = itemsDto.Sum(i => i.LineTotal);
            var tax = subTotal * TaxRate;

            return new CartSummaryDto
            {
                Items = itemsDto,
                SubTotal = subTotal,
                Tax = tax,
                Total = subTotal + tax,
                ItemCount = itemsDto.Sum(i => i.Quantity)
            };
        }

        public async Task<bool> AddToCartAsync(string userId, AddToCartDto addDto)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var product = await _context.Products.FindAsync(addDto.ProductId);

            if (product == null || product.IsDeleted || !product.IsAvailable) return false;
            if (product.StockQuantity < addDto.Quantity) return false;

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addDto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += addDto.Quantity;
                existingItem.AddedAt = DateTime.UtcNow;
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = addDto.ProductId,
                    UserId = userId,
                    Quantity = addDto.Quantity,
                    AddedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(newItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCartAsync(string userId, int cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem == null) return false;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> CheckoutAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            if (!cart.CartItems.Any()) return null;

            // 1. Create Order
            var order = new Order
            {
                UserId = userId,
                PlacedAt = DateTime.UtcNow,
                Status = OrderStatus.Confirmed,
                TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity) * (1 + TaxRate)
            };

            // 2. Create Order Items and update Stock
            foreach (var ci in cart.CartItems)
            {
                if (ci.Product.StockQuantity < ci.Quantity)
                    throw new Exception($"Product {ci.Product.Name} is out of stock.");

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                });

                // Deduct inventory
                ci.Product.StockQuantity -= ci.Quantity;
                ci.Product.IsAvailable = ci.Product.StockQuantity > 0;
            }

            _context.Orders.Add(order);

            // 3. Clear Cart
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();
            return order.Id;
        }

        private async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}