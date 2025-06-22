using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [ClientAuthorize]
    public class OrderHistoryController : Controller
    {
        public IActionResult OrderHistory()
        {
            return View();
        }
    }
}
