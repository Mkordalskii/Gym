using Gym.Data.Data;
using Gym.Data.Models.Cms;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class PortalPageService : BaseService, IPortalPageService
    {
        public PortalPageService(GymContext context) : base(context)
        {
        }

        public async Task<PortalPage?> GetPublishedPortalPageBySlugAsync(string slug)
        {
            return await _context.PortalPage
                .Where(p => p.IsPublished)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<List<PortalPage>> GetPublishedPortalPagesAsync()
        {
            return await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }
    }
}
