namespace LearnerCenter.API.Models.DTOs
{
    public class CampusDto
    {
        public Guid CampusId { get; set; }
        public string CampusName { get; set; } = string.Empty;
        public string? CampusCode { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}