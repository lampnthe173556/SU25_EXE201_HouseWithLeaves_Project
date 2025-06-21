using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.Session;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class AuthenticationService : IAuthenticationServices
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAccount(UserLoginDtos userLogin)
        {
            var user = await _userService.GetUserByEmailAndPassword(userLogin.Email, userLogin.PasswordHash);
            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetObject("user", user);
            }
            return user != null;
        }

        public async Task<bool> RegisterAccount(UserRegisterDtos userRegister)
        {
            //check email exits
            var userExits = await _userService.GetUserByEmail(userRegister.Email);
            if(userExits != null)
            {
                return false; 
            }
            var user = _mapper.Map<User>(userRegister);
            await _userService.CreateUser(user);
            return true;
        }

    }
}
