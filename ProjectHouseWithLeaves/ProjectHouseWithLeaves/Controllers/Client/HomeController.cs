using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;
using ProjectHouseWithLeaves.Helper.Session;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        // Trang chủ công khai - cho phép tất cả truy cập
        public async Task<IActionResult> Index()
        {
            // Kiểm tra xem user có đăng nhập không
            var user = HttpContext.Session.GetObject<User>("user");
            
            if (user != null)
            {
                // Nếu đã đăng nhập, kiểm tra role
                if (user.RoleId == 2) // Admin
                {
                    // Redirect admin về trang admin
                    return RedirectToAction("Admin", "Admin", new { area = "Admin" });
                }
                else if (user.RoleId == 1) // User thường
                {
                    // User thường vào trang chủ bình thường
                    var productSeller = await _productService.GetProductBestSeller();
                    ViewBag.ProductSeller = productSeller;
                    return View("Home");
                }
            }
            
            // Nếu chưa đăng nhập hoặc role không xác định, hiển thị trang chủ công khai
            var publicProductSeller = await _productService.GetProductBestSeller();
            ViewBag.ProductSeller = publicProductSeller;
            return View("Home");
        }

        // Trang chủ có phân quyền - chỉ user thường mới vào được
        [ClientAuthorize]
        public async Task<IActionResult> Home()
        {
            var productSeller = await _productService.GetProductBestSeller();
            ViewBag.ProductSeller = productSeller;
            return View();
        }
       
        
    }
}
