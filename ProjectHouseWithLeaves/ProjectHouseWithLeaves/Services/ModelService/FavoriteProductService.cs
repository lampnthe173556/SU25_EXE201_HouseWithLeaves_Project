using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly MiniPlantStoreContext _context;

        public FavoriteProductService(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteProduct>> GetAllFavoriteProducts()
        {
            return await _context.FavoriteProducts
                .Include(fp => fp.Product)
                .Include(fp => fp.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteProduct>> GetTopFavoriteProducts(int count = 5)
        {
            return await _context.FavoriteProducts
                .Include(fp => fp.Product)
                .GroupBy(fp => fp.ProductId)
                .Select(g => new FavoriteProduct
                {
                    ProductId = g.Key,
                    Product = g.First().Product,
                    CreatedAt = g.Max(fp => fp.CreatedAt)
                })
                .OrderByDescending(fp => fp.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetFavoriteProductCount()
        {
            return await _context.FavoriteProducts.CountAsync();
        }
    }
} 