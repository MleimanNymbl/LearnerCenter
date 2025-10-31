namespace LearnerCenter.API.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Models.Course>> GetCoursesByEnrollmentIdAsync(Guid enrollmentId);
        Task<Models.Course?> GetCourseByIdAsync(Guid courseId);
        Task<IEnumerable<Models.Course>> GetAllCoursesAsync();
    }
}