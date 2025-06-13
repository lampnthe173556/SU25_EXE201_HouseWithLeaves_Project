using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductBestSeller();
        public Task<IEnumerable<ProductShopDtos>> GetAllProduct();
        public Task<Product?> GetProductById(int id);
        public Task<IEnumerable<Product>> GetProductSame(int productId);
        //admin
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
