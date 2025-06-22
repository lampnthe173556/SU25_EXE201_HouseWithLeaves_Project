using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Helper.Authorization;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [ClientAuthorize]
    public class OrderHistoryController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderHistoryController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult OrderHistory()
        {
            return View();
        }
        public async Task<IActionResult> OrderHistoryById()
        {
            var userId = HttpContext.Session.GetObject<User>("user").UserId;
            var orderHistoryListByUserId = await _orderService.GetOrderById(userId);
            return Json(orderHistoryListByUserId);
        }
    }
}
