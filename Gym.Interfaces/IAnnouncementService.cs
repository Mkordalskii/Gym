using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<Announcement>> GetActiveAnnouncementsAsync();
        Task<Announcement?> GetLatestAnnouncementAsync();
    }
}
