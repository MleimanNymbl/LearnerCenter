using System.Security.Cryptography;
using System.Text;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public UserService(IUserRepository userRepository, IEnrollmentRepository enrollmentRepository)
        {
            _userRepository = userRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<UserRegistrationResponseDto> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            // Check if user already exists
            if (await _userRepository.UserExistsAsync(registrationDto.Username, registrationDto.Email))
            {
                throw new InvalidOperationException("Username or email already exists.");
            }

            // Validate enrollment exists
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(registrationDto.EnrollmentId);
            if (enrollment == null)
            {
                throw new InvalidOperationException("Invalid enrollment ID.");
            }

            // Hash the password
            var passwordHash = HashPassword(registrationDto.Password);

            // Create User
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                PasswordHash = passwordHash,
                EnrollmentId = registrationDto.EnrollmentId,
                CreatedDate = DateTime.UtcNow,
                Status = "Active",
                IsActive = true
            };

            // Create User in database
            var createdUser = await _userRepository.CreateUserAsync(user);

            // Create UserProfile
            var userProfile = new UserProfile
            {
                UserProfileId = Guid.NewGuid(),
                UserId = createdUser.UserId,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                PhoneNumber = registrationDto.PhoneNumber,
                Address = registrationDto.Address,
                City = registrationDto.City,
                State = registrationDto.State,
                ZipCode = registrationDto.ZipCode,
                DateOfBirth = registrationDto.DateOfBirth,
                Gender = registrationDto.Gender,
                EmergencyContactName = registrationDto.EmergencyContactName,
                EmergencyContactPhone = registrationDto.EmergencyContactPhone,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            // Create UserProfile in database
            await _userRepository.CreateUserProfileAsync(userProfile);

            return new UserRegistrationResponseDto
            {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Message = "User registered successfully."
            };
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return user == null ? null : MapToUserDto(user);
        }

        public async Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user == null ? null : MapToUserDto(user);
        }

        public async Task<UserExistsDto> CheckUserExistsAsync(string username, string email)
        {
            var (usernameExists, emailExists) = await _userRepository.CheckUserExistsDetailedAsync(username, email);
            var exists = usernameExists || emailExists;
            
            return new UserExistsDto 
            { 
                Exists = exists,
                UsernameExists = usernameExists,
                EmailExists = emailExists
            };
        }

        public async Task<IEnumerable<UserSummaryDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(MapToUserSummaryDto);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Add a simple salt (in production, use a proper random salt per user)
                var saltedPassword = password + "LearnerCenter2024!";
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Helper method for future login functionality
        public bool VerifyPassword(string password, string hash)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hash;
        }

        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                EnrollmentId = user.EnrollmentId,
                Profile = user.UserProfile == null ? null : new UserProfileDto
                {
                    FirstName = user.UserProfile.FirstName,
                    LastName = user.UserProfile.LastName,
                    PhoneNumber = user.UserProfile.PhoneNumber,
                    Address = user.UserProfile.Address,
                    City = user.UserProfile.City,
                    State = user.UserProfile.State,
                    ZipCode = user.UserProfile.ZipCode,
                    DateOfBirth = user.UserProfile.DateOfBirth,
                    Gender = user.UserProfile.Gender,
                    EmergencyContactName = user.UserProfile.EmergencyContactName,
                    EmergencyContactPhone = user.UserProfile.EmergencyContactPhone
                },
                Enrollment = user.CurrentEnrollment == null ? null : new UserEnrollmentDto
                {
                    EnrollmentId = user.CurrentEnrollment.EnrollmentId,
                    ProgramName = user.CurrentEnrollment.ProgramName,
                    Degree = user.CurrentEnrollment.Degree,
                    Description = user.CurrentEnrollment.Description,
                    IsActive = user.CurrentEnrollment.IsActive
                }
            };
        }

        private UserSummaryDto MapToUserSummaryDto(User user)
        {
            return new UserSummaryDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                LastLoginDate = user.LastLoginDate,
                EnrollmentId = user.EnrollmentId,
                ProfileName = user.UserProfile != null 
                    ? $"{user.UserProfile.FirstName} {user.UserProfile.LastName}" 
                    : "No Profile"
            };
        }
    }
}