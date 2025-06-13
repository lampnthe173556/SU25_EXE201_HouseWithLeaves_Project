using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IUserService
    {
        Task<User?>GetUserByEmail(string email); 
        Task CreateUser(User user);
        Task<User?>GetUserByEmailAndPassword(string email, string password);
        Task ChangePasswordAsync(string email, string newPassword);
        Task<User?>UpdateUserProfileClient(UserUpdateProfileDtos userUpdateProfileDtos);
    }
}
