using Gym.Data.Data;
using Gym.Data.Models.Core;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class FitnessClassService : BaseService, IFitnessClassService
    {
        public FitnessClassService(GymContext context) : base(context)
        {
        }

        public async Task<List<FitnessClass>> GetActiveFitnessClassesAsync()
        {
            return await _context.FitnessClass
                .Where(fc => fc.IsActive)
                .OrderBy(fc => fc.StartTime)
                .ToListAsync();
        }

        public async Task<List<FitnessClass>> GetUpcomingFitnessClassesAsync(int count)
        {
            return await _context.FitnessClass
                .Where(fc => fc.IsActive)
                .OrderBy(fc => fc.StartTime)
                .Take(count)
                .ToListAsync();
        }
    }
}
