using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class OrderHistoryController : Controller
    {
        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}
