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
            // TEMP: brak logowania / fake user - bierzemy pierwszego (jedynego) membera
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
    }
}
