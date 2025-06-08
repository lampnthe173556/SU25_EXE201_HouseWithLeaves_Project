using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductBestSeller();
    }
}
