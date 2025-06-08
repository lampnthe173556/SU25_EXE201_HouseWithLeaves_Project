using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;

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

        public async Task<IActionResult> Home()
        {
            var productSeller = await _productService.GetProductBestSeller();
            ViewBag.ProductSeller = productSeller;
            return View();
        }
       
        
    }
}
