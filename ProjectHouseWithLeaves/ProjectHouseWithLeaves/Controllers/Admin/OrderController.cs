using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Models;
using System.Text.Json;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class OrderController : Controller
    {
        private readonly MiniPlantStoreContext _context;

        public OrderController(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Order()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.PaymentMethod)
                .Include(o => o.ShippingMethod)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductImages)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.PaymentMethod)
                .Include(o => o.ShippingMethod)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderData = new
            {
                orderId = order.OrderId,
                orderDate = order.OrderDate,
                subtotalAmount = order.SubtotalAmount,
                discountAppliedAmount = order.DiscountAppliedAmount,
                shippingCostApplied = order.ShippingCostApplied,
                totalAmount = order.TotalAmount,
                status = order.Status,
                shippingAddress = order.ShippingAddress,
                user = order.User != null ? new
                {
                    email = order.User.Email,
                    phone = order.User.Phone
                } : null,
                paymentMethod = order.PaymentMethod != null ? new
                {
                    methodName = order.PaymentMethod.MethodName
                } : null,
                shippingMethod = order.ShippingMethod != null ? new
                {
                    methodName = order.ShippingMethod.MethodName
                } : null,
                orderDetails = order.OrderDetails.Select(od => new
                {
                    quantity = od.Quantity,
                    unitPrice = od.UnitPrice,
                    product = od.Product != null ? new
                    {
                        productName = od.Product.ProductName,
                        mainImage = od.Product.ProductImages.FirstOrDefault(pi => pi.MainPicture == true)?.ImageUrl,
                        size = od.Product.Size
                    } : null
                }).ToList()
            };

            return Json(orderData);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đơn hàng" });
                }

                // Validate status transition
                var validTransitions = new Dictionary<string, string[]>
                {
                    ["Pending"] = new[] { "Processing", "Cancelled" },
                    ["Processing"] = new[] { "Shipped", "Cancelled" },
                    ["Shipped"] = new[] { "Delivered", "Returned" },
                    ["Delivered"] = new[] { "Returned" },
                    ["Cancelled"] = new string[0],
                    ["Returned"] = new string[0]
                };

                if (order.Status != null && validTransitions.ContainsKey(order.Status))
                {
                    var allowedTransitions = validTransitions[order.Status];
                    if (!allowedTransitions.Contains(status))
                    {
                        return Json(new { success = false, message = "Không thể chuyển từ trạng thái này sang trạng thái đã chọn" });
                    }
                }

                order.Status = status;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }
}
