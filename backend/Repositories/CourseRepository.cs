using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LearnerCenterDbContext _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(LearnerCenterDbContext context, ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Course>> GetCoursesByEnrollmentIdAsync(Guid enrollmentId)
        {
            try
            {
                _logger.LogInformation("Fetching courses for enrollment ID: {EnrollmentId}", enrollmentId);

                var courses = await _context.Courses
                    .Where(c => c.EnrollmentId == enrollmentId && c.IsActive)
                    .OrderBy(c => c.CourseName)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {CourseCount} courses for enrollment {EnrollmentId}", 
                    courses.Count, enrollmentId);

                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching courses for enrollment {EnrollmentId}", enrollmentId);
                throw;
            }
        }

        public async Task<Models.Course?> GetCourseByIdAsync(Guid courseId)
        {
            try
            {
                _logger.LogInformation("Fetching course with ID: {CourseId}", courseId);

                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == courseId && c.IsActive);

                if (course != null)
                {
                    _logger.LogInformation("Found course: {CourseName}", course.CourseName);
                }
                else
                {
                    _logger.LogWarning("Course with ID {CourseId} not found", courseId);
                }

                return course;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching course {CourseId}", courseId);
                throw;
            }
        }

        public async Task<IEnumerable<Models.Course>> GetAllCoursesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all active courses");

                var courses = await _context.Courses
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.CourseName)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {CourseCount} active courses", courses.Count);

                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all courses");
                throw;
            }
        }
    }
}