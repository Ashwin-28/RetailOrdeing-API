namespace RetailAPP_API.DTOs.ProductDTOs
{
    public class UpdateStockDto
    {
        public int NewStockQuantity { get; set; }
        public string? Reason { get; set; }   // e.g. 'Restock from supplier'

    }
}
