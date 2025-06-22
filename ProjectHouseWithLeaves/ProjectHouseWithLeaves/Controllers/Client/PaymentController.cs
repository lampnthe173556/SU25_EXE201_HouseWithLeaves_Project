using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [ClientAuthorize]
    public class PaymentController : Controller
    {
        private readonly IShippingMethodService _shippingMethodService;

        public PaymentController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }
        public IActionResult Paypal()
        {
            return View();
        }
        public IActionResult PaymentCommon()
        {
            return View();
        }
        public async Task<IActionResult> GetShippingMethodJson()
        {
            var shipping = await _shippingMethodService.GetAllShippingMethod();
            return Json(shipping);
        }
    }
}
