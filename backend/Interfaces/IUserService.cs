using LearnerCenter.API.Models;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Interfaces
{
    public interface IUserService
    {
        Task<UserRegistrationResponseDto> RegisterUserAsync(UserRegistrationDto registrationDto);
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task<UserDto?> GetUserByUsernameAsync(string username);
        Task<UserExistsDto> CheckUserExistsAsync(string username, string email);
        Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync();
    }
}