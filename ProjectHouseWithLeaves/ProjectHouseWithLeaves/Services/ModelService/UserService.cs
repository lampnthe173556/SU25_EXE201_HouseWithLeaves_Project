using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHouseWithLeaves.Dtos;
using ProjectHouseWithLeaves.Helper.PasswordHasing;
using ProjectHouseWithLeaves.Models;

namespace ProjectHouseWithLeaves.Services.ModelService
{
    public class UserService : IUserService
    {
        private readonly MiniPlantStoreContext _context;
        private readonly IMapper _mapper;

        public UserService(MiniPlantStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task ChangePasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            user.PasswordHash = PasswordHassing.ComputeSha256Hash(newPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();
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
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }
        
        public async Task UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Update fields
            //existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                existingUser.PasswordHash = PasswordHassing.ComputeSha256Hash(user.PasswordHash);
            }
            existingUser.UpdatedAt = DateTime.UtcNow;

            // Save changes
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UpdateUserProfileClient(UserUpdateProfileDtos userUpdateProfileDtos)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email.ToLower() == userUpdateProfileDtos.Email.ToLower());
            _mapper.Map(userUpdateProfileDtos, user);
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
