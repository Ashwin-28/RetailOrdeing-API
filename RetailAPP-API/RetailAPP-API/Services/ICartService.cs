using RetailAPP_API.DTOs.CartDTOs;

namespace RetailAPP_API.Services
{
    public interface ICartService
    {
        Task<CartSummaryDto> GetCartSummaryAsync(string userId);
        Task<bool> AddToCartAsync(string userId, AddToCartDto addDto);
        Task<bool> RemoveFromCartAsync(string userId, int cartItemId);
        Task<int?> CheckoutAsync(string userId);
    }
}