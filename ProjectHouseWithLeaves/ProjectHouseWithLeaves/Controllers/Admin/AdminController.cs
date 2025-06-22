using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly ILogger<AdminController> _logger;
        private readonly MiniPlantStoreContext _context;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService,
            ILogger<AdminController> logger,
            IUserService userService,
            IOrderService orderService,
            IFavoriteProductService favoriteProductService,
            MiniPlantStoreContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _userService = userService;
            _orderService = orderService;
            _favoriteProductService = favoriteProductService;
            _context = context;
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

                var orders = await _orderService.GetAllOrders();
                ViewBag.OrderCount = orders.Count();

                // Get top selling products based on order details
                var topSellingProducts = await GetTopSellingProducts(5);
                ViewBag.TopSellingProducts = topSellingProducts;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data: {Message}", ex.Message);
                // Set default values if there's an error
                ViewBag.ProductCount = 0;
                ViewBag.CategoryCount = 0;
                ViewBag.TopSellingProducts = new List<dynamic>();
                // Add error message to TempData to display to user
                TempData["ErrorMessage"] = "Không thể tải dữ liệu dashboard. Vui lòng thử lại sau.";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRevenueData(string view = "daily")
        {
            try
            {
                // Get accepted orders with their details
                var acceptedOrders = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => o.Status == "ACCEPTED" && o.OrderDate.HasValue)
                    .ToListAsync();

                var totalRevenue = acceptedOrders.Sum(o => o.TotalAmount ?? 0);
                var totalOrders = acceptedOrders.Count;
                var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

                var revenueData = new
                {
                    totalRevenue = totalRevenue,
                    totalOrders = totalOrders,
                    avgOrderValue = avgOrderValue,
                    labels = new List<string>(),
                    values = new List<decimal>()
                };

                if (view == "daily")
                {
                    // Group by day for the last 5 days
                    var endDate = DateTime.Now.Date;
                    var startDate = endDate.AddDays(-4);

                    var dailyRevenue = acceptedOrders
                        .Where(o => o.OrderDate.Value.Date >= startDate && o.OrderDate.Value.Date <= endDate)
                        .GroupBy(o => o.OrderDate.Value.Date)
                        .Select(g => new
                        {
                            Date = g.Key,
                            Revenue = g.Sum(o => o.TotalAmount ?? 0)
                        })
                        .OrderBy(x => x.Date)
                        .ToList();

                    // Fill missing days with 0 revenue
                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        var dayRevenue = dailyRevenue.FirstOrDefault(d => d.Date == date);
                        revenueData.labels.Add(date.ToString("dd/MM"));
                        revenueData.values.Add(dayRevenue?.Revenue ?? 0);
                    }
                }
                else // weekly
                {
                    // Group by week for the last 3 weeks
                    var endDate = DateTime.Now.Date;
                    var startDate = endDate.AddDays(-20); // 3 weeks * 7 days - 1 day

                    var weeklyRevenue = acceptedOrders
                        .Where(o => o.OrderDate.Value.Date >= startDate && o.OrderDate.Value.Date <= endDate)
                        .GroupBy(o => GetWeekStart(o.OrderDate.Value))
                        .Select(g => new
                        {
                            WeekStart = g.Key,
                            Revenue = g.Sum(o => o.TotalAmount ?? 0)
                        })
                        .OrderBy(x => x.WeekStart)
                        .ToList();

                    // Fill missing weeks with 0 revenue
                    for (var weekStart = startDate; weekStart <= endDate; weekStart = weekStart.AddDays(7))
                    {
                        var weekRevenue = weeklyRevenue.FirstOrDefault(w => w.WeekStart == weekStart);
                        revenueData.labels.Add($"Tuần {GetWeekNumber(weekStart)}");
                        revenueData.values.Add(weekRevenue?.Revenue ?? 0);
                    }
                }

                return Json(revenueData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting revenue data: {Message}", ex.Message);
                return Json(new
                {
                    totalRevenue = 0,
                    totalOrders = 0,
                    avgOrderValue = 0,
                    labels = new List<string>(),
                    values = new List<decimal>()
                });
            }
        }

        private DateTime GetWeekStart(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private int GetWeekNumber(DateTime date)
        {
            var startOfYear = new DateTime(date.Year, 1, 1);
            var days = (date - startOfYear).Days;
            return (days / 7) + 1;
        }

        private async Task<List<dynamic>> GetTopSellingProducts(int count = 5)
        {
            try
            {
                var topSellingProducts = await _context.OrderDetails
                    .Include(od => od.Product)
                    .Where(od => od.Product != null && !od.Product.IsDeleted.Value)
                    .GroupBy(od => od.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        ProductName = g.First().Product.ProductName,
                        Price = g.First().Product.Price,
                        TotalSold = g.Sum(od => od.Quantity)
                    })
                    .OrderByDescending(p => p.TotalSold)
                    .Take(count)
                    .ToListAsync();

                return topSellingProducts.Cast<dynamic>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting top selling products");
                return new List<dynamic>();
            }
        }

        public IActionResult RedirectedFromClient()
        {
            TempData["ErrorMessage"] = "Admin không được phép truy cập trang client! Vui lòng sử dụng các chức năng quản trị.";
            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> Index()
        {
            // Redirect về trang admin dashboard
            return RedirectToAction("Admin");
        }

    }
} 