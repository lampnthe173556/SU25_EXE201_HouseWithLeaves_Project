using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Helper.Authorization
{
    public class ClientAuthorizationFilter : IAuthorizationFilter
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
            if (user.RoleId == 2)
            {
                // Nếu là admin, redirect về trang admin với thông báo
                context.Result = new RedirectToActionResult("RedirectedFromClient", "Admin", new { area = "Admin" });
                return;
            }

            // Nếu là user thường (RoleId = 1), cho phép truy cập
            // Có thể thêm kiểm tra thêm nếu cần
        }
    }
} 