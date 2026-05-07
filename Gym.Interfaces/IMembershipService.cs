using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public interface IMembershipService
    {
        Task<Membership?> GetActiveMembershipForCurrentMemberAsync();
    }
}
