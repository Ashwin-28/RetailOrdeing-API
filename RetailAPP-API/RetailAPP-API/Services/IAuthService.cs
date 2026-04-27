using RetailAPP_API.DTOs.AuthDTOs;

namespace RetailAPP_API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto model);
        Task<AuthResponseDto> LoginAsync(LoginDto model);
    }
}
