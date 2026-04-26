using Gym.Data.Models.Core;

namespace Gym.Admin.Models.Home
{
    public class AdminDashboardViewModel
    {
        public Dictionary<string, string> Parameters { get; set; } = new();

        public int ActiveMembersCount { get; set; }
        public int TotalMembersCount { get; set; }

        public int ActiveMembershipsCount { get; set; }
        public int MembershipsExpiringSoonCount { get; set; }

        public int TodayBookingsCount { get; set; }
        public int TodayCancelledBookingsCount { get; set; }

        public int CmsParametersCount { get; set; }
        public int CmsPagesCount { get; set; }

        public List<FitnessClass> UpcomingClasses { get; set; } = new();
    }
}
