using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Helper.PasswordHasing;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class UserService : IUserService
    {
        private readonly MiniPlantStoreContext _context;

        public UserService(MiniPlantStoreContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            user.PasswordHash = PasswordHassing.ComputeSha256Hash(user.PasswordHash);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();  
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return user;
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() 
                && x.PasswordHash == PasswordHassing.ComputeSha256Hash(password));
            return user;
        }
    }
}
