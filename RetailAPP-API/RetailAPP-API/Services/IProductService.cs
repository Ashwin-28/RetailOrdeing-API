using RetailAPP_API.DTOs.ProductDTOs;

namespace RetailAPP_API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(int? categoryId);
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto createDto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto updateDto);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateStockAsync(int id, UpdateStockDto stockDto);
    }
}
