using LearnerCenter.API.Models.DTOs;

namespace LearnerCenter.API.Interfaces
{
    public interface ICampusService
    {
        Task<IEnumerable<CampusDto>> GetAllActiveCampusesAsync();
        Task<CampusDto?> GetCampusByIdAsync(Guid campusId);
    }
}