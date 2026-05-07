using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public interface IFitnessClassService
    {
        Task<List<FitnessClass>> GetActiveFitnessClassesAsync();
        Task<List<FitnessClass>> GetUpcomingFitnessClassesAsync(int count);
    }
}
