using Gym.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly GymContext _context;

        public PageController(GymContext context)
        {
            _context = context;
        }

        [HttpGet("page/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var page = await _context.PortalPage
                .Where(p => p.IsPublished)
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (page == null)
            {
                return NotFound();
            }

            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();

            ViewBag.Parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);

            return View(page);
        }
    }
}
