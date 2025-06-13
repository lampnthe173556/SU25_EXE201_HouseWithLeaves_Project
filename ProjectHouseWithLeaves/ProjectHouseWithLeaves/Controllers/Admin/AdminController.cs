using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<AdminController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Starting to load dashboard data");

                // Get product count
                var products = await _productService.GetAllProducts();
                _logger.LogInformation($"Retrieved {products.Count()} products");
                ViewBag.ProductCount = products.Count();

                // Get category count
                var categories = await _categoryService.GetAllCategory();
                _logger.LogInformation($"Retrieved {categories.Count()} categories");
                ViewBag.CategoryCount = categories.Count();

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data: {Message}", ex.Message);
                // Set default values if there's an error
                ViewBag.ProductCount = 0;
                ViewBag.CategoryCount = 0;
                // Add error message to TempData to display to user
                TempData["ErrorMessage"] = "Không thể tải dữ liệu dashboard. Vui lòng thử lại sau.";
                return View();
            }
        }

    }
} 