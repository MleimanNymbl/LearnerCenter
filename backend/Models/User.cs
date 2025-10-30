using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnerCenter.API.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Graduated, Withdrawn, Suspended

        [Required]
        public bool IsActive { get; set; } = true;

        // Optional enrollment - users can exist without being enrolled in a program
        [ForeignKey("Enrollment")]
        public Guid? EnrollmentId { get; set; }

        // Navigation properties
        public virtual UserProfile? UserProfile { get; set; }
        public virtual Enrollment? CurrentEnrollment { get; set; }
    }
}