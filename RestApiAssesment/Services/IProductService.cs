using RestApiAssesment.Model;

namespace RestApiAssesment.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task DecrementStockAsync(int productId, int quantity);
        Task AddToStockAsync(int productId, int quantity);
    }
}
