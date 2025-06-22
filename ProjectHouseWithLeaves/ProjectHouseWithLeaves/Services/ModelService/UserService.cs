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
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 8)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự");
            }
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            user.PasswordHash = PasswordHassing.ComputeSha256Hash(newPassword);
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateUser(User user)
        {
            if (string.IsNullOrEmpty(user.PasswordHash) || user.PasswordHash.Length < 8)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 8 ký tự");
            }
            
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
                    .FirstOrDefaultAsync(u => u.UserId == id);

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
                    Gender = user.Gender,
                    Roles = new List<string> { user.Role?.RoleName ?? "Chưa có vai trò" },
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

        public async Task<User> UpdateUser(UserUpdateDTO userDto)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == userDto.Id);

                if (existingUser == null)
                {
                    throw new KeyNotFoundException($"User with ID {userDto.Id} not found");
                }

                // Update basic info (không thay đổi email)
                existingUser.FullName = userDto.FullName;
                existingUser.Phone = userDto.Phone;
                existingUser.Gender = userDto.Gender == "OTHER" ? null : userDto.Gender;
                existingUser.Status = userDto.IsActive ? 1 : 0;
                existingUser.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();

                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID {userDto.Id}");
                throw;
            }
        }

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
    }
}
