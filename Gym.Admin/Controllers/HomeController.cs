using System.Diagnostics;
using Gym.Admin.Models;
using Gym.Admin.Models.Home;
using Gym.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Admin.Controllers
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
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var sevenDaysFromNow = today.AddDays(7);

            var parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);

            var model = new AdminDashboardViewModel
            {
                Parameters = parameters,

                ActiveMembersCount = await _context.Member.CountAsync(m => m.IsActive),
                TotalMembersCount = await _context.Member.CountAsync(),

                ActiveMembershipsCount = await _context.Membership
                    .CountAsync(m => m.IsActive && m.EndDate >= today),

                MembershipsExpiringSoonCount = await _context.Membership
                    .CountAsync(m => m.IsActive && m.EndDate >= today && m.EndDate <= sevenDaysFromNow),

                TodayBookingsCount = await _context.Booking
                    .CountAsync(b => b.IsActive && b.CreatedAt >= today && b.CreatedAt < tomorrow),

                TodayCancelledBookingsCount = await _context.Booking
                    .CountAsync(b => b.IsActive && b.Status == "Cancelled" && b.CreatedAt >= today && b.CreatedAt < tomorrow),

                CmsParametersCount = await _context.Parameter.CountAsync(p => p.IsActive),
                CmsPagesCount = await _context.PortalPage.CountAsync(p => p.IsActive),

                UpcomingClasses = await _context.FitnessClass
                    .Include(fc => fc.Bookings)
                    .Where(fc => fc.IsActive && fc.StartTime >= today && fc.StartTime < tomorrow)
                    .OrderBy(fc => fc.StartTime)
                    .Take(5)
                    .ToListAsync()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
