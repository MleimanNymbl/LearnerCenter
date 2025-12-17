using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<EnrollmentDto?> GetEnrollmentByIdAsync(Guid enrollmentId);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsByCampusAsync(Guid campusId);
        Task<bool> ProcessPaymentAsync(Guid enrollmentId, decimal amount);
    }
}