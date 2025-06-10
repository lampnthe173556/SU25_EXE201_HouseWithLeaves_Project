using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email); 
        Task CreateUser(User user);
        Task<User?> GetUserByEmailAndPassword(string email, string password);
    }
}
