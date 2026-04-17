using System.Diagnostics;
using Gym.Data.Data;
using Gym.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GymContext _context;

        public HomeController(GymContext context)
        {
            _context = context;
        }
        [HttpGet("{slug?}")]
        public async Task<IActionResult> Index(string? slug)
        {
            var pages = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();

            ViewBag.PageModel = pages;
            ViewBag.Announcements = await _context.Announcement
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
            ViewBag.MembershipPlans = await _context.MembershipPlan
                .Where(mp => mp.IsActive)
                .OrderBy(mp => mp.Price)
                .ToListAsync();
            await LoadParametersAsync();

            if (string.IsNullOrEmpty(slug))
            {
                var first = pages.FirstOrDefault();
                slug = first?.Slug;
            }

            if (string.IsNullOrEmpty(slug))
                return NotFound();

            var item = await _context.PortalPage.FirstOrDefaultAsync(p => p.Slug == slug);
            if (item == null) return NotFound();

            return View(item);
        }

        public async Task<IActionResult> Privacy()
        {
            await LoadParametersAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task LoadParametersAsync()
        {
            ViewBag.Parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);
        }
    }
}

