using Microsoft.AspNetCore.Mvc;
using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all courses for a specific enrollment
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID to filter courses</param>
        /// <returns>List of courses for the specified enrollment</returns>
        [HttpGet("enrollment/{enrollmentId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetCoursesByEnrollment(Guid enrollmentId)
        {
            try
            {
                _logger.LogInformation("API request received: Get courses for enrollment {EnrollmentId}", enrollmentId);

                var courses = await _courseService.GetCoursesByEnrollmentIdAsync(enrollmentId);
                
                _logger.LogInformation("API response: Retrieved {CourseCount} courses for enrollment {EnrollmentId}", 
                    courses.Count(), enrollmentId);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for enrollment {EnrollmentId}", enrollmentId);
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }

        /// <summary>
        /// Gets all courses
        /// </summary>
        /// <returns>List of all courses</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllCourses()
        {
            try
            {
                _logger.LogInformation("API request received: Get all courses");

                var courses = await _courseService.GetAllCoursesAsync();
                
                _logger.LogInformation("API response: Retrieved {CourseCount} courses", courses.Count());

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all courses");
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }

        /// <summary>
        /// Gets a specific course by ID
        /// </summary>
        /// <param name="courseId">The course ID</param>
        /// <returns>The course details</returns>
        [HttpGet("{courseId}")]
        public async Task<ActionResult<object>> GetCourseById(Guid courseId)
        {
            try
            {
                _logger.LogInformation("API request received: Get course {CourseId}", courseId);

                var course = await _courseService.GetCourseByIdAsync(courseId);
                
                if (course == null)
                {
                    _logger.LogWarning("Course {CourseId} not found", courseId);
                    return NotFound($"Course with ID {courseId} not found");
                }

                _logger.LogInformation("API response: Retrieved course {CourseId}", courseId);

                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course {CourseId}", courseId);
                return StatusCode(500, "An error occurred while retrieving the course");
            }
        }
    }
}