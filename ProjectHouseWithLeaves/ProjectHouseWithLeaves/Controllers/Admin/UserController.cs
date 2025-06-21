using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;
using ProjectHouseWithLeaves.Services.ModelService;
using ProjectHouseWithLeaves.Helper.Authorization;

namespace ProjectHouseWithLeaves.Controllers.Admin
{
    [Area("Admin")]
    [AdminAuthorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: admin/users
        [HttpGet]
        public async Task<IActionResult> User()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
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

        // PUT: admin/users/update
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserDTO userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedUser = await _userService.UpdateUser(userUpdateDto);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        // PATCH: admin/users/toggle-status/{id}
        [HttpPatch("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                await _userService.ToggleUserStatus(id);
                return Ok("User status toggled successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}