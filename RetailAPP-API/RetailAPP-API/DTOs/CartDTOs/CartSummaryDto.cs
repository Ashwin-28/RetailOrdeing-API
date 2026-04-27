namespace RetailAPP_API.DTOs.CartDTOs
{
    public class CartSummaryDto
    {
        public List<CartItemResponseDto> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }             // e.g. 18% GST
        public decimal Total { get; set; }
        public int ItemCount { get; set; }

    }
}
