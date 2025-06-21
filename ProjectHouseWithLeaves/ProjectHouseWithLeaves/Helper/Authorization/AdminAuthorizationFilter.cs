using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Authorization
{
    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Kiểm tra xem user có đăng nhập không
            var user = context.HttpContext.Session.GetObject<User>("user");
            
            if (user == null)
            {
                // Nếu chưa đăng nhập, redirect về trang login
                context.Result = new RedirectToActionResult("Auth", "Authen", new { area = "" });
                return;
            }

            // Kiểm tra xem user có phải là admin không (RoleId = 2 là admin)
            if (user.RoleId != 2)
            {
                // Nếu không phải admin, redirect về trang chủ
                context.Result = new RedirectToActionResult("Home", "Home", new { area = "" });
                return;
            }

            // Nếu là admin, cho phép truy cập
        }
    }
} 