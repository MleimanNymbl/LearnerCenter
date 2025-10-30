using LearnerCenter.API.DTOs;

namespace LearnerCenter.API.Interfaces
{
    /// <summary>
    /// Interface for Campus service operations
    /// Defines business logic methods for Campus-related operations
    /// </summary>
    public interface ICampusService
    {
        /// <summary>
        /// Gets all active campuses
        /// </summary>
        /// <returns>Collection of CampusDto objects</returns>
        Task<IEnumerable<CampusDto>> GetAllActiveCampusesAsync();

        /// <summary>
        /// Gets a specific campus by ID
        /// </summary>
        /// <param name="campusId">The campus ID to search for</param>
        /// <returns>CampusDto if found, null otherwise</returns>
        Task<CampusDto?> GetCampusByIdAsync(Guid campusId);
    }
}