using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Authorization;
using ProjectHouseWithLeaves.Helper.PasswordHasing;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangepasswordDtos request)
        {
            try
            {
                // Lấy thông tin user hiện tại từ session
                var currentUser = HttpContext.Session.GetObject<User>("user");
                if (currentUser == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng" });
                }

                // Kiểm tra mật khẩu hiện tại
                var hashedCurrentPassword = PasswordHassing.ComputeSha256Hash(request.oldPassword ?? "");
                if (currentUser.PasswordHash != hashedCurrentPassword)
                {
                    return Json(new { success = false, message = "Mật khẩu hiện tại không đúng" });
                }

                // Kiểm tra mật khẩu mới
                if (string.IsNullOrEmpty(request.newPassword) || request.newPassword.Length < 8)
                {
                    return Json(new { success = false, message = "Mật khẩu mới phải có ít nhất 8 ký tự" });
                }

                // Kiểm tra xác nhận mật khẩu
                if (request.newPassword != request.confirmPassword)
                {
                    return Json(new { success = false, message = "Mật khẩu mới và xác nhận mật khẩu không khớp" });
                }

                // Cập nhật mật khẩu
                await _userService.ChangePasswordAsync(currentUser.Email, request.newPassword);

                // Cập nhật session với thông tin mới
                var updatedUser = await _userService.GetUserByEmail(currentUser.Email);
                if (updatedUser != null)
                {
                    HttpContext.Session.SetObject("user", updatedUser);
                }
                
                _logger.LogInformation($"User {currentUser.UserId} changed password successfully");
                return Json(new { success = true, message = "Đổi mật khẩu thành công" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user");
                return Json(new { success = false, message = "Có lỗi xảy ra khi đổi mật khẩu" });
            }
        }
    }
} 