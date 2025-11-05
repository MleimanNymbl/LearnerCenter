using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LearnerCenterDbContext _context;

        public UserRepository(LearnerCenterDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.UserProfile)
                .Include(u => u.CurrentEnrollment)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserProfile)
                .Include(u => u.CurrentEnrollment)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserProfile)
                .Include(u => u.CurrentEnrollment)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<(bool UsernameExists, bool EmailExists)> CheckUserExistsDetailedAsync(string username, string email)
        {
            bool usernameExists = false;
            bool emailExists = false;

            if (!string.IsNullOrWhiteSpace(username))
            {
                usernameExists = await _context.Users
                    .AnyAsync(u => u.Username == username);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                emailExists = await _context.Users
                    .AnyAsync(u => u.Email == email);
            }

            return (usernameExists, emailExists);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return userProfile;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExistsByIdAsync(user.UserId))
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<bool> UpdateLastLoginDateAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.LastLoginDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserProfile)
                .Include(u => u.CurrentEnrollment)
                .ToListAsync();
        }

        private async Task<bool> UserExistsByIdAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(e => e.UserId == userId);
        }
    }
}