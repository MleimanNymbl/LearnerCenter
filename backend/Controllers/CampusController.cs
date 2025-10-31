using Microsoft.AspNetCore.Mvc;
using LearnerCenter.API.Models.DTOs;
using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusController : ControllerBase
    {
        private readonly ICampusService _campusService;
        private readonly ILogger<CampusController> _logger;

        public CampusController(ICampusService campusService, ILogger<CampusController> logger)
        {
            _campusService = campusService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampusDto>>> GetCampuses()
        {
            try
            {
                _logger.LogInformation("API request received: Get all campuses");

                var campuses = await _campusService.GetAllActiveCampusesAsync();

                _logger.LogInformation("API response: Retrieved {Count} campuses", campuses.Count());
                return Ok(campuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error occurred while retrieving campuses");
                return StatusCode(500, "An error occurred while retrieving campuses");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampusDto>> GetCampus(Guid id)
        {
            try
            {
                _logger.LogInformation("API request received: Get campus with ID: {CampusId}", id);

                var campus = await _campusService.GetCampusByIdAsync(id);

                if (campus == null)
                {
                    _logger.LogWarning("API response: Campus with ID {CampusId} not found", id);
                    return NotFound($"Campus with ID {id} not found");
                }

                _logger.LogInformation("API response: Retrieved campus: {CampusName}", campus.CampusName);
                return Ok(campus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API error occurred while retrieving campus with ID: {CampusId}", id);
                return StatusCode(500, "An error occurred while retrieving the campus");
            }
        }
    }
}