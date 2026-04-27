using Microsoft.AspNetCore.Identity;

namespace RetailAPP_API.Models;

public class ApplicationUser : IdentityUser
{
    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
