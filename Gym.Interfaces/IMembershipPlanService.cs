using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public interface IMembershipPlanService
    {
        Task<List<MembershipPlan>> GetActiveMembershipPlansAsync();
    }
}
