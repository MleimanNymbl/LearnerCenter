using LearnerCenter.API.DTOs;

namespace LearnerCenter.API.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto?> GetEnrollmentByIdAsync(Guid enrollmentId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCampusAsync(Guid campusId);
    }
}