using Microsoft.AspNetCore.Identity;
using RetailAPP_API.DTOs.AuthDTOs;
using RetailAPP_API.Models;

namespace RetailAPP_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new AuthResponseDto { IsSuccess = false, Message = "User already exists!" };

            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponseDto { IsSuccess = false, Message = $"User creation failed! {errors}" };
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));

            await _userManager.AddToRoleAsync(user, model.Role);

            return new AuthResponseDto { IsSuccess = true, Message = "User created successfully!" };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Simple token for fresher level implementation
                return new AuthResponseDto 
                { 
                    IsSuccess = true, 
                    Message = "Login successful!", 
                    Token = "Simple-Token-Placeholder-Configure-JWT-If-Needed" 
                };
            }
            return new AuthResponseDto { IsSuccess = false, Message = "Invalid login attempt!" };
        }
    }
}
