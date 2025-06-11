using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Services.EmailService;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Session;
using System.Threading.Tasks;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ForgetpasswordController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public ForgetpasswordController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string? email)
        {
            var user = await _userService.GetUserByEmail(email);
            if(user != null)
            {
               HttpContext.Session.SetObject("user", user);
               await _emailService.SendOtpByEmailAsync(email);
            }
            return View("ForgetPassword");
        }
        public IActionResult VerifyOTP()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifyOTP(string otp)
        {
            var otpSaveInSession = HttpContext.Session.GetObject<OtpDataDtos>("Otp");
            if(otpSaveInSession != null && otpSaveInSession.Code == otp)
            {
                if(otpSaveInSession.Expiration > DateTime.UtcNow)
                {
                    HttpContext.Session.Remove("Otp");
                    return RedirectToAction("NewPassword", "Forgetpassword");
                }
                else
                {
                    ViewBag.ErrorOtp = "Otp hết hiệu lực";
                }
            }
            else
            {
                ViewBag.ErrorOtp = "Otp sai";
                
            }
            return View();
        }
        public IActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewPassword(string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                ViewBag.ErrorNewPass = "Password not confirm";
            }
            else
            {
                var user = HttpContext.Session.GetObject<User>("user");
                await _userService.ChangePasswordAsync(user.Email, newPassword);
                HttpContext.Session.RemoveObject("user");
                return RedirectToAction("Auth", "Authen");
            }
            return View();
        }
    }
}
