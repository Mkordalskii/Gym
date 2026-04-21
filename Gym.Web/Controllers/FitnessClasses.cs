using Gym.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class FitnessClasses : Controller
    {
        private readonly GymContext _context;
        public FitnessClasses(GymContext context)
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

            var classes = await _context.FitnessClass
                .Where(c => c.IsActive)
                .OrderBy(c => c.StartTime)
                .ToListAsync();

            return View(classes);
        }
    }
}
