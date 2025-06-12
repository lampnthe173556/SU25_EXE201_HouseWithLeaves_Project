using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using System.Threading.Tasks;

namespace ProjectHouseWithLeaves.Controllers.Client
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Profile()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserUpdateProfileDtos profile)
        {
            var user = await _userService.UpdateUserProfileClient(profile);
            HttpContext.Session.SetObject("user", user);
            TempData["ProfileMessage"] = "Cập nhật thông tin thành công!";
            TempData["ProfileMessageType"] = "success";
            return RedirectToAction("Profile");
        }
    }
}
