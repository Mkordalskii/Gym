using Gym.Data.Data;
using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly GymContext _context;
        private readonly IParameterService _parameterService;

        public PageController(GymContext context, IParameterService parameterService)
        {
            _context = context;
            _parameterService = parameterService;
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

            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            return View(page);
        }
    }
}
