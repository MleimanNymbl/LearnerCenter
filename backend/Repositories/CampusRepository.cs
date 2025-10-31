using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Repositories
{
    public class CampusRepository : ICampusRepository
    {
        private readonly LearnerCenterDbContext _context;
        private readonly ILogger<CampusRepository> _logger;

        public CampusRepository(LearnerCenterDbContext context, ILogger<CampusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Campus>> GetAllActiveCampusesAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all active campuses from database");
                
                var campuses = await _context.Campuses
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.CampusName)
                    .ToListAsync();

                _logger.LogInformation("Retrieved {Count} active campuses from database", campuses.Count);
                return campuses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching campuses from database");
                throw;
            }
        }
        public async Task<Campus?> GetCampusByIdAsync(Guid campusId)
        {
            try
            {
                _logger.LogInformation("Fetching campus with ID: {CampusId} from database", campusId);
                
                var campus = await _context.Campuses
                    .Where(c => c.CampusId == campusId && c.IsActive)
                    .FirstOrDefaultAsync();

                if (campus == null)
                {
                    _logger.LogWarning("Campus with ID {CampusId} not found in database", campusId);
                }
                else
                {
                    _logger.LogInformation("Retrieved campus: {CampusName} from database", campus.CampusName);
                }

                return campus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching campus with ID: {CampusId} from database", campusId);
                throw;
            }
        }
    }
}