using LearnerCenter.API.Models;

namespace LearnerCenter.API.Interfaces
{
    public interface ICampusRepository
    {
        Task<IEnumerable<Campus>> GetAllActiveCampusesAsync();
        Task<Campus?> GetCampusByIdAsync(Guid campusId);
    }
}