namespace RetailAPP_API.DTOs.OrderDTOs
{
    public class PlaceOrderDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public string? Notes { get; set; }
        // Cart items are read from the DB — user cannot fake quantities

    }
}
