namespace RetailAPP_API.Models.Enums
{
    public enum OrderStatus
    {
        Pending,       // order placed, not yet confirmed
        Confirmed,     // admin confirmed the order
        Shipped,       // order shipped
        Delivered,     // order delivered to customer
        Cancelled      // order cancelled (stock gets restored!)
    }
}
