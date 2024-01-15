using PetStoreBackend.Dtos;
using PetStoreBackend.Models;

namespace PetStoreBackend.Interfaces
{
    public interface IProductRepository
    {
        public Task ProductDtoToProduct(ProductDto productDto, Product product);
        public Task<ICollection<Product>> GetProductsAsync();
        public Task<ICollection<ProductDto>> GetProductsDtoAsync(HttpRequest request);
        public Task<Product> GetProductAsync(int Id);
        public Task<bool> CreateProductAsync(Product product);
        public Task<bool> UpdateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int Id);
        public Task<bool> SaveAsync();
        public Task<string> SaveImageAsync(IFormFile formFile);
        public void DeleteImage(string imageName);
    }
}
