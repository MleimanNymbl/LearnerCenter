using LearnerCenter.API.Models;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);
        Task<(bool UsernameExists, bool EmailExists)> CheckUserExistsDetailedAsync(string username, string email);
        Task<User> CreateUserAsync(User user);
        Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile);
        Task<User?> UpdateUserAsync(User user);
        Task<bool> UpdateLastLoginDateAsync(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}