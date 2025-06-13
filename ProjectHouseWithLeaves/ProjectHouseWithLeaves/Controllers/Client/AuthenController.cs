using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;
using System.Linq;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class AuthenController : Controller
    {
        private readonly IAuthenticationServices _authenticationService;

        public AuthenController(IAuthenticationServices authenticationService)
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
            else
            {
                var user = HttpContext.Session.GetObject<User>("user");
                if(user.RoleId != 1)
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                return RedirectToAction("home", "home");
            }
            
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.RemoveObject("user");
            return View("Auth");
        }

        //google
        [HttpGet]
        public IActionResult GoogleLogin()
        {           
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleCallback") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet]
        public async Task<IActionResult> GoogleCallback()
        {
            // Debug: Kiểm tra cookie correlation Google
            var correlationCookie = Request.Cookies.FirstOrDefault(c => c.Key.Contains("AspNetCore.Correlation.Google"));
            if (correlationCookie.Key != null)
            {
                Console.WriteLine($"[DEBUG] Correlation Cookie FOUND: {correlationCookie.Key} = {correlationCookie.Value}");
            }
            else
            {
                Console.WriteLine("[DEBUG] Correlation Cookie NOT FOUND!");
            }

            // 1. Lấy thông tin người dùng từ cookie tạm thời mà middleware đã tạo
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (result?.Succeeded != true)
            {
                ViewBag.MessageLogin = "Đăng nhập với Google thất bại.";
                return View("Auth");
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 5. Chuyển hướng đến trang chủ
            return RedirectToAction("Home", "Home");
        }
    }
}
