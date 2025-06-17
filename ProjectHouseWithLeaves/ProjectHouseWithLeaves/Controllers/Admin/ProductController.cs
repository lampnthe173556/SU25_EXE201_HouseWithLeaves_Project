using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Services.StoreService;
using Microsoft.Extensions.Configuration;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<ProductController> logger,
            IWebHostEnvironment webHostEnvironment,
            IStorageService storageService,
            IConfiguration configuration)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string searchTerm = "", int? categoryId = null)
        {
            try
            {
                var products = await _productService.GetAllProducts();
                var categories = await _categoryService.GetAllCategory();

                // Lọc sản phẩm theo từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(p =>
                        p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        (p.Description != null && p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                        (p.Size != null && p.Size.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Lọc theo danh mục
                if (categoryId.HasValue)
                {
                    products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                }

                ViewBag.SearchTerm = searchTerm;
                ViewBag.CategoryId = categoryId;
                ViewBag.CategoryList = categories;

                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading products page");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải trang sản phẩm";
                return View(new List<ProductDTO>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTO productDto, IFormFile mainImage, List<IFormFile> additionalImages)
        {
            try
            {
                var product = new Product
                {
                    ProductName = productDto.Name,
                    Description = productDto.Description,
                    Size = productDto.Size,
                    Price = productDto.Price,
                    QuantityInStock = productDto.Stock,
                    CategoryId = productDto.CategoryId,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    ProductImages = new List<ProductImage>()
                };

                // Xử lý ảnh chính
                if (mainImage != null)
                {
                    var mainImageUrl = await _storageService.UploadFileAsync(mainImage);
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = mainImageUrl,
                        MainPicture = true
                    });
                }
                else
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = _configuration["R2:PublicUrlBase"] + "/avatar-mac-dinh-4.jpg",
                        MainPicture = true
                    });
                }

                // Xử lý ảnh phụ
                if (additionalImages != null && additionalImages.Any())
                {
                    foreach (var image in additionalImages.Take(2))
                    {
                        var imageUrl = await _storageService.UploadFileAsync(image);
                        product.ProductImages.Add(new ProductImage
                        {
                            ImageUrl = imageUrl,
                            MainPicture = false
                        });
                    }
                }

                // Thêm ảnh mặc định nếu thiếu ảnh phụ
                while (product.ProductImages.Count < 3)
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = _configuration["R2:PublicUrlBase"] + "/avatar-mac-dinh-4.jpg",
                        MainPicture = false
                    });
                }

                await _productService.CreateProduct(product);
                TempData["SuccessMessage"] = "Thêm sản phẩm thành công";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm sản phẩm";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
                }

                var productDto = new ProductDTO
                {
                    Id = product.ProductId,
                    Name = product.ProductName,
                    Description = product.Description,
                    Size = product.Size,
                    Price = product.Price,
                    Stock = product.QuantityInStock,
                    CategoryId = product.CategoryId,
                    MainImage = product.ProductImages.FirstOrDefault(img => img.MainPicture == true)?.ImageUrl,
                    AdditionalImages = product.ProductImages.Where(img => img.MainPicture == false).Select(img => img.ImageUrl).ToList()
                };

                return Json(new { success = true, data = productDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product");
                return Json(new { success = false, message = "Không thể lấy thông tin sản phẩm" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] ProductDTO productDto, IFormFile mainImage, IFormFile additionalImage1, IFormFile additionalImage2)
        {
            try
            {
                var existingProduct = await _productService.GetProductById(productDto.Id);
                if (existingProduct == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
                }

                existingProduct.ProductName = productDto.Name;
                existingProduct.Description = productDto.Description;
                existingProduct.Size = productDto.Size;
                existingProduct.Price = productDto.Price;
                existingProduct.QuantityInStock = productDto.Stock;
                existingProduct.CategoryId = productDto.CategoryId;
                existingProduct.UpdatedAt = DateTime.Now;

                // Xử lý ảnh chính
                if (mainImage != null)
                {
                    var mainImageUrl = await _storageService.UploadFileAsync(mainImage);
                    var mainImageEntity = existingProduct.ProductImages.FirstOrDefault(img => img.MainPicture == true);
                    if (mainImageEntity != null)
                    {
                        mainImageEntity.ImageUrl = mainImageUrl;
                    }
                }

                // Xử lý ảnh phụ 1
                if (additionalImage1 != null)
                {
                    var imageUrl = await _storageService.UploadFileAsync(additionalImage1);
                    var additionalImageEntities = existingProduct.ProductImages.Where(img => img.MainPicture == false).ToList();
                    if (additionalImageEntities.Count > 0)
                    {
                        additionalImageEntities[0].ImageUrl = imageUrl;
                    }
                }

                // Xử lý ảnh phụ 2
                if (additionalImage2 != null)
                {
                    var imageUrl = await _storageService.UploadFileAsync(additionalImage2);
                    var additionalImageEntities = existingProduct.ProductImages.Where(img => img.MainPicture == false).ToList();
                    if (additionalImageEntities.Count > 1)
                    {
                        additionalImageEntities[1].ImageUrl = imageUrl;
                    }
                }

                await _productService.UpdateProduct(existingProduct);
                return Json(new { success = true, message = "Cập nhật sản phẩm thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật sản phẩm" });
            }
        }
    }
} 