using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class PageController : Controller
    {
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;

        public PageController(IParameterService parameterService, IPortalPageService portalPageService)
        {
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }

        [HttpGet("page/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return NotFound();
            }

            var page = await _portalPageService.GetPublishedPortalPageBySlugAsync(slug);

            if (page == null)
            {
                return NotFound();
            }

            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();

            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            return View(page);
        }
    }
}
