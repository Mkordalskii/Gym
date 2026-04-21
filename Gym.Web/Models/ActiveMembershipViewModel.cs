using Gym.Data.Models.Core;

namespace Gym.Web.Models
{
    public class ActiveMembershipViewModel
    {
        public Membership? Membership { get; set; }
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}
