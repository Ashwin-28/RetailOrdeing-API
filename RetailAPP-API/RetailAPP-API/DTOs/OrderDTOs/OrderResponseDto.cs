namespace RetailAPP_API.DTOs.OrderDTOs
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime PlacedAt { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();

    }
}
