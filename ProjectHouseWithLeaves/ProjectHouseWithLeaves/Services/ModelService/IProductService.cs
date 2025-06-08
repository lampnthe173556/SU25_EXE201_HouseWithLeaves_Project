using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductBestSeller();
        public Task<IEnumerable<ProductShopDtos>> GetAllProduct();
    }
}
