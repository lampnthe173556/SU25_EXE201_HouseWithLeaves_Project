using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
