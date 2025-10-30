using LearnerCenter.API.Models;

namespace LearnerCenter.API.Interfaces
{
    /// <summary>
    /// Interface for Campus repository operations
    /// Defines data access methods for Campus entities
    /// </summary>
    public interface ICampusRepository
    {
        /// <summary>
        /// Gets all active campuses from the database
        /// </summary>
        /// <returns>Collection of active Campus entities</returns>
        Task<IEnumerable<Campus>> GetAllActiveCampusesAsync();

        /// <summary>
        /// Gets a specific campus by ID
        /// </summary>
        /// <param name="campusId">The campus ID to search for</param>
        /// <returns>Campus entity if found, null otherwise</returns>
        Task<Campus?> GetCampusByIdAsync(Guid campusId);
    }
}