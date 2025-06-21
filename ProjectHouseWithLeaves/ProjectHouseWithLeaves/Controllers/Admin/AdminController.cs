using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<AdminController> logger,
            IUserService userService,
            IFavoriteProductService favoriteProductService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _userService = userService;
            _favoriteProductService = favoriteProductService;
        }

        public async Task<IActionResult> Admin()
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

                var users =  await _userService.GetAllUsers();
                ViewBag.UserCount = users.Count();

                // Get top favorite products
                var topFavoriteProducts = await _favoriteProductService.GetTopFavoriteProducts(5);
                ViewBag.TopFavoriteProducts = topFavoriteProducts;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data: {Message}", ex.Message);
                // Set default values if there's an error
                ViewBag.ProductCount = 0;
                ViewBag.CategoryCount = 0;
                ViewBag.TopFavoriteProducts = new List<FavoriteProduct>();
                // Add error message to TempData to display to user
                TempData["ErrorMessage"] = "Không thể tải dữ liệu dashboard. Vui lòng thử lại sau.";
                return View();
            }
        }

    }
} 