using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class MembershipPlansController : Controller
    {
        private readonly IMembershipPlanService _membershipPlanService;
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;

        public MembershipPlansController(IMembershipPlanService membershipPlanService, IParameterService parameterService, IPortalPageService portalPageService)
        {
            _membershipPlanService = membershipPlanService;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var plans = await _membershipPlanService.GetActiveMembershipPlansAsync();

            return View(plans);
        }
    }
}
