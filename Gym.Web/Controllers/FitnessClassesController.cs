using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class FitnessClassesController : Controller
    {
        private readonly IFitnessClassService _fitnessClassService;
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;
        public FitnessClassesController(IFitnessClassService fitnessClassService, IParameterService parameterService, IPortalPageService portalPageService)
        {
            _fitnessClassService = fitnessClassService;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var classes = await _fitnessClassService.GetActiveFitnessClassesAsync();

            return View(classes);
        }
    }
}
