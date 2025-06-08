using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
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

        public async Task<IEnumerable<ProductShopDtos>> GetAllProduct()
        {
            var products = await _context.Products
                                        .Include(p => p.ProductImages)
                                        .ToListAsync();
            var productShopDtos = products.Select(p => new ProductShopDtos
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Price = p.Price,
                ImageUrls = p.ProductImages.Select(img => img.ImageUrl).ToList(),
                CategoryId = p.CategoryId,
            });
            return productShopDtos;
        }

        public async Task<IEnumerable<Product>> GetProductBestSeller()
        {
            var cheapestProducts = await _context.Products
                                        .Include(p => p.ProductImages)
                                        .Where(p => p.CategoryId != 6 && p.CategoryId != 7)
                                        .Take(8)
                                        .OrderBy(p => p.Price)
                                        .ToListAsync();
            return cheapestProducts;
        }      
    }
}
