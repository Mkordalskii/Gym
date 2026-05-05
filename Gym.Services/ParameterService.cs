using Gym.Data.Data;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class ParameterService : BaseService, IParameterService
    {
        public ParameterService(GymContext context) : base(context)
        {
        }
        public async Task<Dictionary<string, string>> GetAllActiveParametersAsync()
        {
            return await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);
        }
        public async Task<string> GetParameterValueAsync(string name)
        {
            return await _context.Parameter
                .Where(p => p.IsActive && p.Name == name)
                .Select(p => p.Value)
                .FirstOrDefaultAsync() ?? string.Empty;
        }
    }
}
