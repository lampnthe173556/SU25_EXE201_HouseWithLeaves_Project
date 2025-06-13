using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Dtos;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<ProductController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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

        
    }
} 