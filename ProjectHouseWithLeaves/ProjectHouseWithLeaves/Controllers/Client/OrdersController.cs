using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDtos order)
        {
            order.UserId = HttpContext.Session.GetObject<User>("user").UserId;
            var result = await _orderService.CreateOrder(order);
            if (result)
            {
                return Ok(new { success = true, message = "Order created successfully." });
            }
            return BadRequest(new { success = false, message = "Failed to create order." });
        }
    }
}
