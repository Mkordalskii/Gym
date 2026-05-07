using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;
        public AnnouncementsController(IAnnouncementService announcementService, IParameterService parameterService, IPortalPageService portalPageService)
        {
            _announcementService = announcementService;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();
            var announcements = await _announcementService.GetActiveAnnouncementsAsync();
            return View(announcements);
        }
    }
}
