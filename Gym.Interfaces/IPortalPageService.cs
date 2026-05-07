using Gym.Data.Models.Cms;

namespace Gym.Interfaces
{
    public interface IPortalPageService
    {
        Task<List<PortalPage>> GetPublishedPortalPagesAsync();
        Task<PortalPage?> GetPublishedPortalPageBySlugAsync(string slug);
    }
}
