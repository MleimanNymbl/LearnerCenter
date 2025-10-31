using Microsoft.AspNetCore.Mvc;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly ILogger<EnrollmentController> _logger;

        public EnrollmentController(IEnrollmentService enrollmentService, ICourseService courseService, ILogger<EnrollmentController> logger)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all enrollments for a specific campus
        /// </summary>
        /// <param name="campusId">The campus ID to filter enrollments</param>
        /// <returns>List of enrollments for the specified campus</returns>
        [HttpGet("campus/{campusId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByCampus(Guid campusId)
        {
            try
            {
                var enrollments = await _enrollmentService.GetEnrollmentsByCampusAsync(campusId);
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollments for campus {CampusId}", campusId);
                return StatusCode(500, "An error occurred while retrieving enrollments");
            }
        }

        /// <summary>
        /// Gets all enrollments
        /// </summary>
        /// <returns>List of all enrollments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all enrollments");
                return StatusCode(500, "An error occurred while retrieving enrollments");
            }
        }

        /// <summary>
        /// Gets a specific enrollment by ID
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID</param>
        /// <returns>The enrollment details</returns>
        [HttpGet("{enrollmentId}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(Guid enrollmentId)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(enrollmentId);
                if (enrollment == null)
                {
                    return NotFound($"Enrollment with ID {enrollmentId} not found");
                }
                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving enrollment {EnrollmentId}", enrollmentId);
                return StatusCode(500, "An error occurred while retrieving the enrollment");
            }
        }

        /// <summary>
        /// Gets all courses associated with a specific enrollment
        /// </summary>
        /// <param name="enrollmentId">The enrollment ID</param>
        /// <returns>List of courses for the specified enrollment</returns>
        [HttpGet("{enrollmentId}/courses")]
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
    }
}