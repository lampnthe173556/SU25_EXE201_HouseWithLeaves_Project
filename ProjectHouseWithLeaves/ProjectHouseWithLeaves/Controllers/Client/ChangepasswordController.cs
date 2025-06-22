using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.PasswordHasing;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    [ClientAuthorize]
    public class ChangepasswordController : Controller
    {
        private readonly IUserService _userService;

        public ChangepasswordController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Changepassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Changepassword(ChangepasswordDtos changepassword)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            if(user.PasswordHash != PasswordHassing.ComputeSha256Hash(changepassword.oldPassword))
            {
                ViewBag.ChangePassMessage = "Mật khẩu cũ không đúng";
                ViewBag.ChangePassType = "error";
            }
            else if (string.IsNullOrEmpty(changepassword.newPassword) || changepassword.newPassword.Length < 8)
            {
                ViewBag.ChangePassMessage = "Mật khẩu mới phải có ít nhất 8 ký tự";
                ViewBag.ChangePassType = "error";
            }
            else
            {
                await _userService.ChangePasswordAsync(user.Email, changepassword.newPassword);
                ViewBag.ChangePassMessage = "Thay đổi mật khẩu thành công";
                ViewBag.ChangePassType = "success";
            }
            return View();
        }
    }
}
