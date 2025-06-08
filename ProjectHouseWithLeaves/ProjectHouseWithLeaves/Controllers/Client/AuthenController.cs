using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class AuthenController : Controller
    {
        public IActionResult Auth()
        {
            return View();
        }
    }
}
