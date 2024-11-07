using RestApiAssesment.Model;
using RestApiAssesment.Repositories;

namespace RestApiAssesment.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductIdGeneratorService _idGenerator;

        public ProductService(IProductRepository productRepository, ProductIdGeneratorService idGenerator)
        {
            _productRepository = productRepository;
            _idGenerator = idGenerator;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            product.Id = _idGenerator.GenerateUniqueProductId();
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task DecrementStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                product.StockAvailable = Math.Max(product.StockAvailable - quantity, 0);
                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task AddToStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                product.StockAvailable += quantity;
                await _productRepository.UpdateAsync(product);
            }
        }
    }
}
