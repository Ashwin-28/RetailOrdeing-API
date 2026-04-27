using Microsoft.AspNetCore.Mvc;
using RetailAPP_API.DTOs.ProductDTOs;
using RetailAPP_API.Services;

namespace RetailAPP_API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts([FromQuery] int? categoryId)
        {
            var products = await _productService.GetAllProductsAsync(categoryId);
            return Ok(products);
        }

        // GET: /api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found." });
            }
            return Ok(product);
        }

        // POST: /api/products
        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdProduct = await _productService.CreateProductAsync(createDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: /api/products/{id}
        // [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _productService.UpdateProductAsync(id, updateDto);
            if (!success) return NotFound(new { message = $"Product with ID {id} not found." });

            return NoContent();
        }

        // DELETE: /api/products/{id}
        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success) return NotFound(new { message = $"Product with ID {id} not found." });

            return NoContent();
        }

        // PUT: /api/products/{id}/stock
        // [Authorize(Roles = "Admin")]
        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDto stockDto)
        {
            var success = await _productService.UpdateStockAsync(id, stockDto);
            if (!success) return NotFound(new { message = $"Product with ID {id} not found." });

            return Ok(new { message = "Stock updated successfully." });
        }
    }
}
