namespace LearnerCenter.API.DTOs
{
    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProgramType { get; set; } = string.Empty;
        public int DurationWeeks { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Campus information
        public Guid CampusId { get; set; }
        public string CampusName { get; set; } = string.Empty;
        public string CampusLocation { get; set; } = string.Empty;

        // Course count for this enrollment
        public int CourseCount { get; set; }
    }
}