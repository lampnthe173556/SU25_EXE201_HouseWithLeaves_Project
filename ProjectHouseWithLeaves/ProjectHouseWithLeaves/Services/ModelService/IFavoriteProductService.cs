using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IFavoriteProductService
    {
        Task<IEnumerable<FavoriteProduct>> GetAllFavoriteProducts();
        Task<IEnumerable<FavoriteProduct>> GetTopFavoriteProducts(int count = 5);
        Task<int> GetFavoriteProductCount();
    }
} 