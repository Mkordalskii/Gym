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

