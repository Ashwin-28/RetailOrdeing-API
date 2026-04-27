namespace RetailAPP_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;     // e.g. Electronics, Clothing
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation — one category has many products
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
