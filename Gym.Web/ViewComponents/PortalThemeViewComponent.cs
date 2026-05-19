using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.ViewComponents
{
    public class PortalThemeViewComponent : ViewComponent
    {
        private readonly IParameterService _parameterService;

        public PortalThemeViewComponent(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var parameters = await _parameterService.GetAllActiveParametersAsync();
            return View(parameters);
        }
    }
}
