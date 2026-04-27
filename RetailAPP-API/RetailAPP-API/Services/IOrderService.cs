using RetailAPP_API.DTOs.OrderDTOs;
using RetailAPP_API.Models.Enums;

namespace RetailAPP_API.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
        Task<OrderDto?> GetOrderByIdAsync(int id, string userId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatusAsync(int id, OrderStatus status);
    }
}
