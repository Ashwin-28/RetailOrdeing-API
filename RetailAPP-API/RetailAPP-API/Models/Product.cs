namespace RetailAPP_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }           // URL or file path to product image

        // ── INVENTORY FIELDS ──────────────────────────────────────────────────
        public int StockQuantity { get; set; }          // current stock
        public int LowStockThreshold { get; set; } = 5; // alert when stock <= this
        public bool IsAvailable { get; set; } = true;

        public bool IsDeleted { get; set; } = false;    // soft delete
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation
        public Category Category { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
