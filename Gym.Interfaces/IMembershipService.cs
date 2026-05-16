using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public enum MembershipOperationStatus
    {
        Success,
        PlanNotFound,
        NoMember
    }

    public record MembershipOperationResult(MembershipOperationStatus Status);

    public interface IMembershipService
    {
        Task<Membership?> GetActiveMembershipForCurrentMemberAsync();
        Task<List<Membership>> GetMembershipsForCurrentMemberAsync();
        Task<MembershipOperationResult> CreateMembershipForCurrentMemberAsync(int membershipPlanId);
    }
}
