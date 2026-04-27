using RetailAPP_API.Models.Enums;

namespace RetailAPP_API.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Shipping info captured at time of order (from checkout form)
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? Notes { get; set; }

        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign key
        public string UserId { get; set; } = string.Empty;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
