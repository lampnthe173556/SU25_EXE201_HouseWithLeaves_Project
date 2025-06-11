using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class AuthenController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public IActionResult Auth()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDtos userRegisterDtos)
        {
            var result = await _authenticationService.RegisterAccount(userRegisterDtos);
            if (result)
            {
                ViewBag.MessageRegister = "Đăng ký thành công";
            }
            else
            {
                ViewBag.MessageRegister = "Đăng ký thất bại!, Email đã bị trùng vui lòng đăng ký với email khác";
            }

                return View("Auth", result);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDtos userLoginDtos)
        {
            var result = await _authenticationService.LoginAccount(userLoginDtos);
            if(result == false)
            {
                ViewBag.MessageLogin = "Đăng nhập thất bại, Email hoặc mật khẩu chưa chính xác";
                return View("Auth", result);
            }
            return RedirectToAction("home","home");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.RemoveObject("user");
            return View("Auth");
        }

    }
}
