using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Data;
using LearnerCenter.API.Interfaces;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly LearnerCenterDbContext _context;

        public EnrollmentRepository(LearnerCenterDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Campus)
                .Include(e => e.Courses)
                .OrderBy(e => e.Campus.CampusName)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByIdAsync(Guid enrollmentId)
        {
            return await _context.Enrollments
                .Include(e => e.Campus)
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.EnrollmentId == enrollmentId);
        }

        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCampusAsync(Guid campusId)
        {
            return await _context.Enrollments
                .Include(e => e.Campus)
                .Include(e => e.Courses)
                .Where(e => e.CampusId == campusId && e.IsActive)
                .OrderBy(e => e.Campus.CampusName)
                .ToListAsync();
        }

        public async Task<bool> UpdateEnrollmentCostAsync(Guid enrollmentId, decimal newCost)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
            if (enrollment == null)
            {
                return false;
            }

            enrollment.Cost = newCost;
            enrollment.UpdatedDate = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}