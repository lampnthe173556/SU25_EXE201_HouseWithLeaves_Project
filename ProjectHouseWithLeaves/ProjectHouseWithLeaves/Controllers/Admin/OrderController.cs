using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Order()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return View(orders);
            }
            catch (Exception ex)
            {
                // Log the exception here if you have logging configured
                return View(new List<ProjectHouseWithLeaves.Models.Order>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid order ID");
            }

            try
            {
                var orderData = await _orderService.GetOrderDataForAdmin(id);
                
                if (orderData == null)
                {
                    return NotFound("Order not found");
                }

                return Json(orderData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error occurred while retrieving order data" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            if (orderId <= 0)
            {
                return Json(new { success = false, message = "Invalid order ID" });
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                return Json(new { success = false, message = "Status cannot be empty" });
            }

            try
            {
                var success = await _orderService.UpdateOrderStatus(orderId, status);
                
                if (success)
                {
                    return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Không thể cập nhật trạng thái. Vui lòng kiểm tra lại quy tắc chuyển đổi trạng thái." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}
