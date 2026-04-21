using Gym.Data.Models.Core;

namespace Gym.Web.Models
{
    public class UpcomingClassesViewModel
    {
        public List<FitnessClass> Classes { get; set; } = new();
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}
