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

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.PlacedAt)
                .ToListAsync();

            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id, string userId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);

            if (order == null) return null;

            return MapToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.PlacedAt)
                .ToListAsync();

            return orders.Select(MapToDto);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
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
