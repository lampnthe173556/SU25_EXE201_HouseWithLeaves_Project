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

        public async Task<Product?> GetProductById(int id)
        {
            var product = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(p => p.ProductId == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductSame(int productId)
        {
            var product = await _context.Products.Include(x => x.ProductImages)
                                .FirstOrDefaultAsync(p => p.ProductId == productId);
            var products = await _context.Products
                                .Include(x => x.ProductImages)
                                .Where(x => (x.Price > product.Price - 20000 && x.Price < product.Price + 20000) 
                                            && x.ProductId != product.ProductId)
                                .ToListAsync(); 
            if(products.Count > 4)
            {
                return products.Take(4);
            }
            return products;
        }
    }
}
