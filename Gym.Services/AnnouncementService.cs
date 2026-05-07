using Gym.Data.Data;
using Gym.Data.Models.Core;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class AnnouncementService : BaseService, IAnnouncementService
    {
        public AnnouncementService(GymContext context) : base(context)
        {
        }

        public async Task<List<Announcement>> GetActiveAnnouncementsAsync()
        {
            return await _context.Announcement
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.PublishFrom)
                .ToListAsync();
        }

        public async Task<Announcement?> GetLatestAnnouncementAsync()
        {
            return await _context.Announcement
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync();
        }
    }
}
