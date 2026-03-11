using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gym.Data.Models.Core;
using Gym.Data.Models.Cms;

namespace Gym.Data.Data
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options)
            : base(options)
        {
        }

        public DbSet<Gym.Data.Models.Core.Announcement> Announcement { get; set; } = default!;

        public DbSet<Gym.Data.Models.Core.Booking> Booking { get; set; } = default!;

        public DbSet<Gym.Data.Models.Core.FitnessClass> FitnessClass { get; set; } = default!;

        public DbSet<Gym.Data.Models.Core.Member> Member { get; set; } = default!;

        public DbSet<Gym.Data.Models.Core.Membership> Membership { get; set; } = default!;

        public DbSet<Gym.Data.Models.Core.MembershipPlan> MembershipPlan { get; set; } = default!;

        public DbSet<Gym.Data.Models.Cms.PortalPage> PortalPage { get; set; } = default!;

        public DbSet<Gym.Data.Models.Cms.PortalText> PortalText { get; set; } = default!;
    }
}
