using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [AdminAuthorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Admin/User
        [HttpGet]
        public async Task<IActionResult> User()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        // GET: /Admin/User/getuser/5
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Json(new
                {
                    id = user.Id,
                    fullName = user.FullName,
                    email = user.Email,
                    phone = user.Phone,
                    gender = user.Gender ?? "OTHER",
                    isActive = user.IsActive
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Error in GetUser(id={id}): {ex}");
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        // POST: admin/users/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Status = userDto.IsActive ? 1 : 0,
                CreatedAt = DateTime.UtcNow
            };

            await _userService.CreateUser(user);
            return Ok("User created successfully");
        }

        // PUT: /Admin/User/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateDTO userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                // Trả về chi tiết lỗi validation để dễ debug
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = await _userService.UpdateUser(userUpdateDto);
                return Ok(new { message = "User updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex}");
                return StatusCode(500, new { message = "An error occurred while updating the user." });
            }
        }

        // DELETE: admin/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: admin/users/toggle-status
        [HttpPost("ToggleStatus")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                await _userService.ToggleUserStatus(id);
                return Json(new { success = true, message = "Thay đổi trạng thái thành công" });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = $"Không tìm thấy người dùng với ID: {id}" });
            }
            catch (Exception ex)
            {
                // Log lỗi để debug
                Console.WriteLine($"Error toggling user status: {ex.Message}");
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }
    }
}