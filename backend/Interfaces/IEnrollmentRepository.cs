using LearnerCenter.API.Models;

namespace LearnerCenter.API.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(Guid enrollmentId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByCampusAsync(Guid campusId);
    }
}