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
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();
            ViewBag.Announcements = await _context.Announcement
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
            ViewBag.MembershipPlans = await _context.MembershipPlan
                .Where(mp => mp.IsActive)
                .OrderBy(mp => mp.Price)
                .ToListAsync();
            ViewBag.NextClass = await _context.FitnessClass
                .Where(fc => fc.IsActive)
                .OrderBy(fc => fc.StartTime)
                .FirstOrDefaultAsync();
            ViewBag.ActiveMembership = await _context.Membership
                .Include(m => m.MembershipPlan)
                .Where(m => m.IsActive && m.EndDate >= DateTime.Now)
                .OrderByDescending(m => m.EndDate)
                .FirstOrDefaultAsync();
            ViewBag.UpcomingClasses = await _context.FitnessClass
                .Where(fc => fc.IsActive)
                .OrderBy(fc => fc.StartTime)
                .Take(3)
                .ToListAsync();

            await LoadParametersAsync();
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await LoadParametersAsync();
            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task LoadParametersAsync()
        {
            ViewBag.Parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);
        }
    }
}

