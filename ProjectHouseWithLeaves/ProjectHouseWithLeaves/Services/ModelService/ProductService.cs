using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class ProductService : IProductService
    {
        private readonly MiniPlantStoreContext _context;

        public ProductService(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductBestSeller()
        {
            var cheapestProducts = await _context.Products
                                        .Where(p => p.CategoryId != 6 && p.CategoryId != 7)
                                        .Take(8)
                                        .OrderBy(p => p.Price)
                                        .ToListAsync();
            return cheapestProducts;
        }      
    }
}
