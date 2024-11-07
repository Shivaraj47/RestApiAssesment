using Microsoft.AspNetCore.Mvc;
using RestApiAssesment.DTO;
using RestApiAssesment.Model;
using RestApiAssesment.Services;

namespace RestApiAssesment.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                StockAvailable = productDto.StockAvailable
            };

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

            // GET /api/products
            [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET /api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT /api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        // PUT /api/products/decrement-stock/{id}/{quantity}
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            await _productService.DecrementStockAsync(id, quantity);
            return Ok();
        }

        // PUT /api/products/add-to-stock/{id}/{quantity}
        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            await _productService.AddToStockAsync(id, quantity);
            return Ok();
        }
    }
}
