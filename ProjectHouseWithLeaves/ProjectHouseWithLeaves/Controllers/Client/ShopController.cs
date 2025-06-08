using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(ILogger<ShopController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Shop()
        {    
            var categories = await _categoryService.GetAllCategory();
            ViewBag.Categories = categories;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAllProductJson()
        {
            var products = await _productService.GetAllProduct();
            return Json(products);
        }
    }
}
