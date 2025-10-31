using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<object>> GetCoursesByEnrollmentIdAsync(Guid enrollmentId)
        {
            try
            {
                _logger.LogInformation("Processing request to get courses for enrollment {EnrollmentId}", enrollmentId);

                var courses = await _courseRepository.GetCoursesByEnrollmentIdAsync(enrollmentId);
                
                var courseDtos = courses.Select(MapToDto);

                _logger.LogInformation("Processed {CourseCount} courses for enrollment {EnrollmentId}", 
                    courseDtos.Count(), enrollmentId);

                return courseDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing courses for enrollment {EnrollmentId}", enrollmentId);
                throw;
            }
        }

        public async Task<object?> GetCourseByIdAsync(Guid courseId)
        {
            try
            {
                _logger.LogInformation("Processing request to get course {CourseId}", courseId);

                var course = await _courseRepository.GetCourseByIdAsync(courseId);
                
                if (course == null)
                {
                    _logger.LogWarning("Course {CourseId} not found", courseId);
                    return null;
                }

                var courseDto = MapToDto(course);

                _logger.LogInformation("Processed course {CourseId}: {CourseName}", courseId, course.CourseName);

                return courseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing course {CourseId}", courseId);
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetAllCoursesAsync()
        {
            try
            {
                _logger.LogInformation("Processing request to get all courses");

                var courses = await _courseRepository.GetAllCoursesAsync();
                
                var courseDtos = courses.Select(MapToDto);

                _logger.LogInformation("Processed {CourseCount} courses", courseDtos.Count());

                return courseDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing all courses");
                throw;
            }
        }

        private static object MapToDto(Models.Course course)
        {
            return new
            {
                courseId = course.CourseId,
                courseCode = course.CourseCode,
                courseName = course.CourseName,
                description = course.Description ?? string.Empty,
                creditHours = course.CreditHours,
                isActive = course.IsActive,
                enrollmentId = course.EnrollmentId
            };
        }
    }
}