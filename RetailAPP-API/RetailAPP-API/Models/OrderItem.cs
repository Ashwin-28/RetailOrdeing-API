namespace RetailAPP_API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }    // price at time of purchase (snapshot)
        public decimal LineTotal => Quantity * UnitPrice;  // computed

        // Foreign keys
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation
        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;

    }
}
