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
        private readonly IPortalPageService _portalPageService;

        public MembershipPlansController(GymContext context, IParameterService parameterService, IPortalPageService portalPageService)
        {
            _context = context;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var plans = await _context.MembershipPlan
                .Where(mp => mp.IsActive)
                .OrderBy(mp => mp.Price)
                .ToListAsync();

            return View(plans);
        }
    }
}
