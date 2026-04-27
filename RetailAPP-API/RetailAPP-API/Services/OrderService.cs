using Microsoft.EntityFrameworkCore;
using RetailAPP_API.Data;
using RetailAPP_API.DTOs.OrderDTOs;
using RetailAPP_API.Models;
using RetailAPP_API.Models.Enums;

namespace RetailAPP_API.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId) =>
            await _context.Orders.Include(o => o.OrderItems)
                .Where(o => o.UserId == userId).OrderByDescending(o => o.PlacedAt)
                .Select(o => MapToDto(o)).ToListAsync();

        public async Task<OrderDto?> GetOrderByIdAsync(int id, string userId)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
            return order != null ? MapToDto(order) : null;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync() =>
            await _context.Orders.Include(o => o.OrderItems)
                .OrderByDescending(o => o.PlacedAt)
                .Select(o => MapToDto(o)).ToListAsync();

        public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return false;

            // If the order is being cancelled, restore the inventory
            if (status == OrderStatus.Cancelled && order.Status != OrderStatus.Cancelled)
            {
                foreach (var item in order.OrderItems.Where(i => i.Product != null))
                {
                    item.Product.StockQuantity += item.Quantity;
                }
            }

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OrderDto?> CreateOrderAsync(string userId, PlaceOrderDto placeOrderDto)
        {
            if (string.IsNullOrWhiteSpace(userId) || placeOrderDto == null || 
                string.IsNullOrWhiteSpace(placeOrderDto.CustomerName) || 
                string.IsNullOrWhiteSpace(placeOrderDto.CustomerPhone) || 
                string.IsNullOrWhiteSpace(placeOrderDto.CustomerAddress)) return null;

            try
            {
                var orderItems = new List<OrderItem>();
                decimal subTotal = 0;

                // Case 1: Items provided in DTO (Direct checkout from frontend cart)
                if (placeOrderDto.Items != null && placeOrderDto.Items.Any())
                {
                    foreach (var itemDto in placeOrderDto.Items)
                    {
                        var product = await _context.Products.FindAsync(itemDto.ProductId);
                        if (product == null || product.IsDeleted || product.StockQuantity < itemDto.Quantity)
                            return null;

                        orderItems.Add(new OrderItem
                        {
                            ProductId = product.Id,
                            Quantity = itemDto.Quantity,
                            UnitPrice = product.Price
                        });

                        subTotal += product.Price * itemDto.Quantity;
                        product.StockQuantity -= itemDto.Quantity;
                    }
                }
                // Case 2: Read from Database Cart
                else
                {
                    var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product)
                        .FirstOrDefaultAsync(c => c.UserId == userId);

                    if (cart == null || !cart.CartItems.Any() || cart.CartItems.Any(ci => ci.Product == null || ci.Product.StockQuantity < ci.Quantity))
                        return null;

                    foreach (var ci in cart.CartItems)
                    {
                        orderItems.Add(new OrderItem
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity,
                            UnitPrice = ci.Product!.Price
                        });
                        subTotal += ci.Product.Price * ci.Quantity;
                        ci.Product.StockQuantity -= ci.Quantity;
                    }
                    _context.CartItems.RemoveRange(cart.CartItems);
                }

                var order = new Order
                {
                    UserId = userId,
                    CustomerName = placeOrderDto.CustomerName,
                    CustomerPhone = placeOrderDto.CustomerPhone,
                    CustomerAddress = placeOrderDto.CustomerAddress,
                    TotalAmount = subTotal, // Tax removed as requested
                    Status = OrderStatus.Pending,
                    Notes = placeOrderDto.Notes,
                    PlacedAt = DateTime.UtcNow,
                    OrderItems = orderItems
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return MapToDto(order);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                CustomerAddress = order.CustomerAddress,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Notes = order.Notes,
                PlacedAt = order.PlacedAt,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }
    }
}
