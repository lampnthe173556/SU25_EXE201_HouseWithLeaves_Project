using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class ProductService : IProductService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(MiniPlantStoreContext context, ILogger<ProductService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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
            if (products.Count > 4)
            {
                return products.Take(4);
            }
            return products;
        }

        //admin
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Where(p => !p.IsDeleted.Value)
                .OrderByDescending(p => p.CreatedAt)
                .Select( p => new ProductDTO
                {
                    Id = p.ProductId,
                    Name = p.ProductName, // Gán Name từ ProductName
                    Description = p.Description,
                    Size = p.Size,
                    Price = p.Price,
                    Stock = p.QuantityInStock,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    MainImage = p.ProductImages
                .Where(img => img.MainPicture.HasValue && img.MainPicture.Value) // Lọc hình ảnh chính
                .Select(img => img.ImageUrl)
                .FirstOrDefault() ?? "/images/default-product.jpg", // Đường dẫn mặc định
                    CreatedAt = p.CreatedAt,
                    IsActive = p.IsActive.Value,
                    IsDeleted = p.IsDeleted.Value
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id && !p.IsDeleted.Value);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            try
            {

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = await _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {product.ProductId} not found");
                }

                // Update properties
                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Size = product.Size;
                existingProduct.Price = product.Price;
                existingProduct.QuantityInStock = product.QuantityInStock;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.UpdatedAt = DateTime.Now;

                // Update images
                if (product.ProductImages != null && product.ProductImages.Any())
                {
                    // Cập nhật ảnh chính
                    var mainImage = product.ProductImages.FirstOrDefault(img => img.MainPicture == true);
                    var existingMainImage = existingProduct.ProductImages.FirstOrDefault(img => img.MainPicture == true);
                    if (mainImage != null && existingMainImage != null)
                    {
                        existingMainImage.ImageUrl = mainImage.ImageUrl;
                    }

                    // Cập nhật ảnh phụ
                    var additionalImages = product.ProductImages.Where(img => img.MainPicture == false).ToList();
                    var existingAdditionalImages = existingProduct.ProductImages.Where(img => img.MainPicture == false).ToList();
                    
                    for (int i = 0; i < Math.Min(additionalImages.Count, existingAdditionalImages.Count); i++)
                    {
                        existingAdditionalImages[i].ImageUrl = additionalImages[i].ImageUrl;
                    }
                }

                await _context.SaveChangesAsync();
                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }

                product.IsDeleted = true;
                product.IsActive = false;
                product.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                throw;
            }
        }

    }
}
