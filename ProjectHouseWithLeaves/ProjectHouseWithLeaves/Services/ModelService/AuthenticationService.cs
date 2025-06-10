using AutoMapper;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
