namespace RetailAPP_API.DTOs.CartDTOs
{
    public class UpdateCartItemDto
    {
        public int Quantity { get; set; }
        public int CartItemId { get; internal set; }
    }
}
