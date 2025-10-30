using System.ComponentModel.DataAnnotations;

namespace LearnerCenter.API.Models
{
    public class Term
    {
        [Key]
        public Guid TermId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string TermName { get; set; } = string.Empty; // e.g., "Fall 2024", "Spring 2025"

        [Required]
        [StringLength(20)]
        public string TermCode { get; set; } = string.Empty; // e.g., "F24", "S25"

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? RegistrationStartDate { get; set; }
        public DateTime? RegistrationEndDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}