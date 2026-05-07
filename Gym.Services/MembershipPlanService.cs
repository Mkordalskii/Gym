using Gym.Data.Data;
using Gym.Data.Models.Core;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class MembershipPlanService : BaseService, IMembershipPlanService
    {
        public MembershipPlanService(GymContext context) : base(context)
        {
        }

        public async Task<List<MembershipPlan>> GetActiveMembershipPlansAsync()
        {
            return await _context.MembershipPlan
                .Where(mp => mp.IsActive)
                .OrderBy(mp => mp.Price)
                .ToListAsync();
        }
    }
}
