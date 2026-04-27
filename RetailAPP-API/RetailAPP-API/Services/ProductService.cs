using Microsoft.EntityFrameworkCore;
using RetailAPP_API.Data;
using RetailAPP_API.DTOs.ProductDTOs;
using RetailAPP_API.Models;

namespace RetailAPP_API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(int? categoryId)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = await query.ToListAsync();

            return products.Select(p => MapToResponseDto(p));
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return product != null ? MapToResponseDto(product) : null;
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createDto)
        {
            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                ImageUrl = createDto.ImageUrl,
                Brand = createDto.Brand,
                Packaging = createDto.Packaging,
                StockQuantity = createDto.StockQuantity,
                LowStockThreshold = createDto.LowStockThreshold,
                CategoryId = createDto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                IsAvailable = createDto.StockQuantity > 0
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Reload to get Category Name
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();

            return MapToResponseDto(product);
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto updateDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted) return false;

            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;
            product.ImageUrl = updateDto.ImageUrl;
            product.Brand = updateDto.Brand;
            product.Packaging = updateDto.Packaging;
            product.StockQuantity = updateDto.StockQuantity;
            product.LowStockThreshold = updateDto.LowStockThreshold;
            product.CategoryId = updateDto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;
            product.IsAvailable = product.StockQuantity > 0;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted) return false;

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStockAsync(int id, UpdateStockDto stockDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted) return false;

            product.StockQuantity = stockDto.NewStockQuantity;
            product.IsAvailable = product.StockQuantity > 0;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        private static ProductResponseDto MapToResponseDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Brand = product.Brand,
                Packaging = product.Packaging,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable,
                IsLowStock = product.StockQuantity <= product.LowStockThreshold,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "Unknown"
            };
        }
    }
}
