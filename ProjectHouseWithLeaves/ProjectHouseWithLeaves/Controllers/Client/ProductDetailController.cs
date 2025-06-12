using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ProductDetailController : Controller
    {
        private readonly IProductService _productService;
        public ProductDetailController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("/ProductDetail/Index/{productId}")]
        public async Task<IActionResult> Index([FromRoute] int productId)
        {
            var productsSame =await _productService.GetProductSame(productId);
            ViewBag.productSame = productsSame;
            ViewData["ProductId"] = productId;
            return View();
        }
        [HttpGet("/ProductDetail/GetJson/{productId}")]
        public async Task<IActionResult> GetProductByJson([FromRoute] int productId)
        {
            var product = await _productService.GetProductById(productId);
            return Json(product);
        }
    }
}
