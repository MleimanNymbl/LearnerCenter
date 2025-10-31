namespace LearnerCenter.API.Models.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Guid? EnrollmentId { get; set; }
        public UserProfileDto? Profile { get; set; }
        public UserEnrollmentDto? Enrollment { get; set; }
    }

    public class UserProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }

    public class UserEnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public string ProgramName { get; set; } = string.Empty;
        public string? Degree { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserSummaryDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Guid? EnrollmentId { get; set; }
        public string ProfileName { get; set; } = string.Empty;
    }

    public class UserExistsDto
    {
        public bool Exists { get; set; }
        public bool UsernameExists { get; set; }
        public bool EmailExists { get; set; }
    }
}