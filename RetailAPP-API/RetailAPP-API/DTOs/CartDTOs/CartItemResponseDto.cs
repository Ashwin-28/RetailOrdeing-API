namespace RetailAPP_API.DTOs.CartDTOs
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }       // UnitPrice * Quantity
        public int AvailableStock { get; set; }

    }
}
