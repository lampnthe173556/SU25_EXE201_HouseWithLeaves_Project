using Microsoft.AspNetCore.Mvc;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class PaymentController : Controller
    {
        public IActionResult Paypal()
        {
            return View();
        }
        public IActionResult PaymentCommon()
        {
            return View();
        }
    }
}
