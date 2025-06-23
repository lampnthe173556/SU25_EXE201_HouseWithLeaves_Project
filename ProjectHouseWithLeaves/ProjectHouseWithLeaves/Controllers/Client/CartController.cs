using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos.Request;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [Route("Cart")]
    [ClientAuthorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IPaymentMethodService _paymentMethodService;

        public CartController(ICartService cartService, IPaymentMethodService paymentMethodService)
        {
            _cartService = cartService;
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        [Route("GetCartData/{userId}")]
        public async Task<IActionResult> GetCartByUser([FromRoute]int userId)
        {
            var cart = await _cartService.GetOrCreareCartByUserAsync(userId);
            return Json(cart);
        }
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemCartRequest request)
        {
            var userId = HttpContext.Session.GetObject<User>("user").UserId;
            var result = await _cartService.AddItemToCartAsync(userId, request.ProductId, request.Quantity);
            return Json(result);
        }
        [HttpPost("DeleteQuantityItem")]
        public async Task<IActionResult> DeleteQuantityItem([FromBody] ItemCartRequest request)
        {
            var userId = HttpContext.Session.GetObject<User>("user").UserId;
            var result = await _cartService.DecreaseItemCartAsync(userId, request.ProductId, request.Quantity);
            return Json(result); 
        }
        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveCartItemRequest request)
        {
            var userId = HttpContext.Session.GetObject<User>("user").UserId;
            var result = await _cartService.RemoveItemFromCartAsync(userId, request.ProductId);
            return Json(result); 
        }
        [HttpPost("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = HttpContext.Session.GetObject<User>("user").UserId;
            var result = await _cartService.ClearCartAsync(userId);
            return Json(result); 
        }
        [HttpGet("GellAllPayment")]
        public async Task<IActionResult> GetAllPayment()
        {
            var payments = await _paymentMethodService.GetAllPaymentMethod();
            return Json(payments);
        }
    }
}
