using Gym.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly GymContext _context;
        public AnnouncementsController(GymContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();
            ViewBag.Parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);
            var announcements = await _context.Announcement
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.PublishFrom)
                .ToListAsync();
            return View(announcements);
        }
    }
}
