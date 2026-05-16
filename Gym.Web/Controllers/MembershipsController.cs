using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;

        public MembershipsController(
            IMembershipService membershipService,
            IParameterService parameterService,
            IPortalPageService portalPageService)
        {
            _membershipService = membershipService;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var memberships = await _membershipService.GetMembershipsForCurrentMemberAsync();

            return View(memberships);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int membershipPlanId)
        {
            var result = await _membershipService.CreateMembershipForCurrentMemberAsync(membershipPlanId);

            switch (result.Status)
            {
                case MembershipOperationStatus.PlanNotFound:
                    return NotFound();
                case MembershipOperationStatus.NoMember:
                    return BadRequest("No member");
                case MembershipOperationStatus.Success:
                    TempData["Success"] = "Membership activated.";
                    return RedirectToAction("Index", "Home");
                default:
                    return RedirectToAction("Index", "MembershipPlans");
            }
        }
    }
}
