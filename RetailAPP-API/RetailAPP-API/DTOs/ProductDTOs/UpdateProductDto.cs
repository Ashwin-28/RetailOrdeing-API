namespace RetailAPP_API.DTOs.ProductDTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Packaging { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }

    }
}
