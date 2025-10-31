using LearnerCenter.API.DTOs;
using LearnerCenter.API.Interfaces;

namespace LearnerCenter.API.Services
{
    public class CampusService : ICampusService
    {
        private readonly ICampusRepository _campusRepository;
        private readonly ILogger<CampusService> _logger;

        public CampusService(ICampusRepository campusRepository, ILogger<CampusService> logger)
        {
            _campusRepository = campusRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CampusDto>> GetAllActiveCampusesAsync()
        {
            try
            {
                _logger.LogInformation("Processing request to get all active campuses");
                
                var campuses = await _campusRepository.GetAllActiveCampusesAsync();
                
                var campusDtos = campuses.Select(c => new CampusDto
                {
                    CampusId = c.CampusId,
                    CampusName = c.CampusName,
                    CampusCode = c.CampusCode,
                    Address = c.Address,
                    City = c.City,
                    State = c.State,
                    ZipCode = c.ZipCode,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    IsActive = c.IsActive,
                    CreatedDate = c.CreatedDate
                }).ToList();

                _logger.LogInformation("Processed {Count} campuses for response", campusDtos.Count);
                return campusDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing campus data");
                throw;
            }
        }

        public async Task<CampusDto?> GetCampusByIdAsync(Guid campusId)
        {
            try
            {
                _logger.LogInformation("Processing request to get campus with ID: {CampusId}", campusId);
                
                var campus = await _campusRepository.GetCampusByIdAsync(campusId);
                
                if (campus == null)
                {
                    _logger.LogWarning("Campus with ID {CampusId} not found", campusId);
                    return null;
                }

                var campusDto = new CampusDto
                {
                    CampusId = campus.CampusId,
                    CampusName = campus.CampusName,
                    CampusCode = campus.CampusCode,
                    Address = campus.Address,
                    City = campus.City,
                    State = campus.State,
                    ZipCode = campus.ZipCode,
                    PhoneNumber = campus.PhoneNumber,
                    Email = campus.Email,
                    IsActive = campus.IsActive,
                    CreatedDate = campus.CreatedDate
                };

                _logger.LogInformation("Processed campus data for: {CampusName}", campusDto.CampusName);
                return campusDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing campus data for ID: {CampusId}", campusId);
                throw;
            }
        }
    }
}