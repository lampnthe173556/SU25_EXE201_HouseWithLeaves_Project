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
        private readonly ILogger<UserService> _logger;

        public UserService(MiniPlantStoreContext context, IMapper mapper, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
        // admin
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Role)
                    .Where(u => u.Status == 1)
                    .OrderByDescending(u => u.CreatedAt)
                    .Select(u => new UserDTO
                    {
                        Id = u.UserId,
                        Email = u.Email,
                        Phone = u.Phone,
                        FullName = u.FullName,
                        DateOfBirth = u.DateOfBirth,
                        Gender = u.Gender,
                        Roles = new List<string> { u.Role.RoleName },
                        IsActive = u.Status == 1,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.UserId == id && u.Status == 1);

                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found");
                }

                return new UserDTO
                {
                    Id = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Roles = new List<string> { user.Role.RoleName },
                    IsActive = user.Status == 1,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with ID {id}");
                throw;
            }
        }

        //public async Task<User> UpdateUser(UserDTO userUpdateDto)
        //{
        //    /*try
        //    {
        //        var existingUser = await _context.Users
        //            .Include(u => u.Role)
        //            .FirstOrDefaultAsync(u => u.UserId == userUpdateDto.Id && u.Status == 1);

        //        if (existingUser == null)
        //        {
        //            throw new KeyNotFoundException($"User with ID {userUpdateDto.Id} not found");
        //        }

        //        // Update basic info
        //        existingUser.FullName = userUpdateDto.FullName;
        //        existingUser.Email = userUpdateDto.Email;
        //        existingUser.Phone = userUpdateDto.Phone;
        //        existingUser.Status = userUpdateDto.IsActive ? 1 : 0;
        //        existingUser.UpdatedAt = DateTime.UtcNow;

        //        // Update password if provided
        //        if (!string.IsNullOrEmpty(userUpdateDto.Pass))
        //        {
        //            existingUser.PasswordHash = PasswordHassing.ComputeSha256Hash(userUpdateDto.NewPassword);
        //        }

        //        // Update role if provided
        //        if (userUpdateDto.Roles != null && userUpdateDto.Roles.Any())
        //        {
        //            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == userUpdateDto.Roles.First());
        //            if (role != null)
        //            {
        //                existingUser.RoleId = role.RoleId;
        //            }
        //        }

        //        _context.Users.Update(existingUser);
        //        await _context.SaveChangesAsync();

        //        return existingUser;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error updating user with ID {userUpdateDto.Id}");
        //        throw;
        //    }*/
        //}

        public async Task ToggleUserStatus(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found");
                }

                user.Status = user.Status == 1 ? 0 : 1;
                user.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error toggling status for user with ID {id}");
                throw;
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found");
                }

                user.Status = 0;
                user.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with ID {id}");
                throw;
            }
        }

        public async Task<User?> UpdateUserProfileClient(UserUpdateProfileDtos userUpdateProfileDtos)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email.ToLower() == userUpdateProfileDtos.Email.ToLower());
            _mapper.Map(userUpdateProfileDtos, user);
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return user;
        }

        public Task<User> UpdateUser(UserDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}
