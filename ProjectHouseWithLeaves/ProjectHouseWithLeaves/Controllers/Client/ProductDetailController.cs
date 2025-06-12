using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ProductDetailController : Controller
    {
        [HttpGet("/ProductDetail/Index/{productId}")]
        public IActionResult Index([FromRoute] int productId)
        {
            return View();
        }
    }
}
