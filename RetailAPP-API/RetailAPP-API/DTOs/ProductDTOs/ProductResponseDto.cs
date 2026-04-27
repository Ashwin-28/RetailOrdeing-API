namespace RetailAPP_API.DTOs.ProductDTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Packaging { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsLowStock { get; set; }        // true when stock <= threshold
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

    }
}
