using LearnerCenter.API.DTOs;
using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllEnrollmentsAsync();
            return enrollments.Select(MapToDto);
        }

        public async Task<EnrollmentDto?> GetEnrollmentByIdAsync(Guid enrollmentId)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(enrollmentId);
            return enrollment != null ? MapToDto(enrollment) : null;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCampusAsync(Guid campusId)
        {
            var enrollments = await _enrollmentRepository.GetEnrollmentsByCampusAsync(campusId);
            return enrollments.Select(MapToDto);
        }

        private static EnrollmentDto MapToDto(Models.Enrollment enrollment)
        {
            return new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                Name = enrollment.ProgramName,
                Description = enrollment.Description ?? string.Empty,
                ProgramType = enrollment.Degree ?? "Certificate",
                DurationWeeks = 16, // Default duration - can be added to model later
                Cost = 2500, // Default cost - can be added to model later  
                IsActive = enrollment.IsActive,
                CreatedAt = enrollment.CreatedDate,
                UpdatedAt = enrollment.UpdatedDate ?? enrollment.CreatedDate,
                CampusId = enrollment.CampusId,
                CampusName = enrollment.Campus?.CampusName ?? string.Empty,
                CampusLocation = $"{enrollment.Campus?.City}, {enrollment.Campus?.State}" ?? string.Empty,
                CourseCount = enrollment.Courses?.Count ?? 0
            };
        }
    }
}