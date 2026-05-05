using Gym.Data.Data;
using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class MembershipPlansController : Controller
    {
        private readonly GymContext _context;
        private readonly IParameterService _parameterService;

        public MembershipPlansController(GymContext context, IParameterService parameterService)
        {
            _context = context;
            _parameterService = parameterService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();

            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var plans = await _context.MembershipPlan
                .Where(mp => mp.IsActive)
                .OrderBy(mp => mp.Price)
                .ToListAsync();

            return View(plans);
        }
    }
}
