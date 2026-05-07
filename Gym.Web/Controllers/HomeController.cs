using System.Diagnostics;
using Gym.Interfaces;
using Gym.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;
        private readonly IBookingService _bookingService;
        private readonly IFitnessClassService _fitnessClassService;
        private readonly IMembershipPlanService _membershipPlanService;
        private readonly IAnnouncementService _announcementService;
        private readonly IMembershipService _membershipService;

        public HomeController(
            IParameterService parameterService,
            IPortalPageService portalPageService,
            IBookingService bookingService,
            IFitnessClassService fitnessClassService,
            IMembershipPlanService membershipPlanService,
            IAnnouncementService announcementService,
            IMembershipService membershipService)
        {
            _parameterService = parameterService;
            _portalPageService = portalPageService;
            _bookingService = bookingService;
            _fitnessClassService = fitnessClassService;
            _membershipPlanService = membershipPlanService;
            _announcementService = announcementService;
            _membershipService = membershipService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.LatestAnnouncement = await _announcementService.GetLatestAnnouncementAsync();
            ViewBag.MembershipPlans = await _membershipPlanService.GetActiveMembershipPlansAsync();
            ViewBag.UpcomingBooking = await _bookingService.GetUpcomingBookingForCurrentMemberAsync();
            ViewBag.ActiveMembership = await _membershipService.GetActiveMembershipForCurrentMemberAsync();
            ViewBag.UpcomingClasses = await _fitnessClassService.GetUpcomingFitnessClassesAsync(3);

            await LoadParametersAsync();
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            await LoadParametersAsync();
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task LoadParametersAsync()
        {
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();
        }
    }
}
