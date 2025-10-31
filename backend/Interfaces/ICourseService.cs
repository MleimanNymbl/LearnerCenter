namespace LearnerCenter.API.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<object>> GetCoursesByEnrollmentIdAsync(Guid enrollmentId);
        Task<object?> GetCourseByIdAsync(Guid courseId);
        Task<IEnumerable<object>> GetAllCoursesAsync();
    }
}