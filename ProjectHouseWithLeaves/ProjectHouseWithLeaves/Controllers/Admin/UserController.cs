using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
