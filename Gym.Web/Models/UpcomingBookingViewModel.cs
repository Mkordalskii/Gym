using Gym.Data.Models.Core;

namespace Gym.Web.Models
{
    public class UpcomingBookingViewModel
    {
        public FitnessClass? FitnessClass { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}
