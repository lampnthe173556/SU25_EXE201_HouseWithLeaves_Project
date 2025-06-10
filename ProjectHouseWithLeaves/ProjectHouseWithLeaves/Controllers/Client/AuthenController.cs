using Microsoft.AspNetCore.Mvc;
using ProjectHouseWithLeaves.Dtos;
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
            
            return View("Auth", result);
        }
    }
}
