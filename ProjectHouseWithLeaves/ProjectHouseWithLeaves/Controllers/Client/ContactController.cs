using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
        
    }
}
