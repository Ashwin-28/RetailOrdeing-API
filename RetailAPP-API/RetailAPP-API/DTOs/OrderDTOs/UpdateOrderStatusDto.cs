namespace RetailAPP_API.DTOs.OrderDTOs
{
    public class UpdateOrderStatusDto
    {
        public string Status { get; set; } = string.Empty;
        // Pending / Confirmed / Shipped / Delivered / Cancelled

    }
}
