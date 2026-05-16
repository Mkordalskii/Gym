using Gym.Data.Data;
using Gym.Data.Models.Core;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class MembershipService : BaseService, IMembershipService
    {
        public MembershipService(GymContext context) : base(context)
        {
        }

        public async Task<Membership?> GetActiveMembershipForCurrentMemberAsync()
        {
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return null;
            }

            return await _context.Membership
                .Include(m => m.MembershipPlan)
                .Where(m => m.IsActive
                            && m.MemberId == member.Id
                            && m.EndDate >= DateTime.Now)
                .OrderByDescending(m => m.EndDate)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Membership>> GetMembershipsForCurrentMemberAsync()
        {
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return new List<Membership>();
            }

            return await _context.Membership
                .Include(m => m.MembershipPlan)
                .Where(m => m.IsActive && m.MemberId == member.Id)
                .OrderByDescending(m => m.EndDate)
                .ToListAsync();
        }

        public async Task<MembershipOperationResult> CreateMembershipForCurrentMemberAsync(int membershipPlanId)
        {
            var plan = await _context.MembershipPlan
                .FirstOrDefaultAsync(p => p.Id == membershipPlanId && p.IsActive);

            if (plan == null)
            {
                return new MembershipOperationResult(MembershipOperationStatus.PlanNotFound);
            }

            // TEMP: brak logowania / fake user
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return new MembershipOperationResult(MembershipOperationStatus.NoMember);
            }

            var existingActive = await _context.Membership
                .Where(m => m.IsActive
                            && m.MemberId == member.Id
                            && m.EndDate >= DateTime.Now)
                .ToListAsync();

            foreach (var existing in existingActive)
            {
                existing.IsActive = false;
                existing.Status = "Replaced";
                existing.ModifiedAt = DateTime.UtcNow;
                existing.ModifiedBy = "Web";
            }

            var startDate = DateTime.Today;
            var membership = new Membership
            {
                MemberId = member.Id,
                MembershipPlanId = plan.Id,
                StartDate = startDate,
                EndDate = startDate.AddDays(plan.DurationInDays),
                Status = "Active",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Web"
            };

            _context.Membership.Add(membership);
            await _context.SaveChangesAsync();

            return new MembershipOperationResult(MembershipOperationStatus.Success);
        }
    }
}
