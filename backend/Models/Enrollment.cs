using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnerCenter.API.Models
{

    public class Enrollment
    {
        [Key]
        public Guid EnrollmentId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Campus")]
        public Guid CampusId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProgramName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Degree { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public virtual Campus Campus { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}