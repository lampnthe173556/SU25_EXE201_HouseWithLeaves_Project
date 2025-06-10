using ProjectHouseWithLeaves.Dtos;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IAuthenticationService
    { 
        public Task<bool> RegisterAccount(UserRegisterDtos userRegister);
        //login

        //forgetPassword
    }
}
