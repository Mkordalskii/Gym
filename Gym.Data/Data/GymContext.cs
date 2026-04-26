using Gym.Data.Models.Cms;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Gym.Data.Models.Cms.Parameter> Parameter { get; set; } = default!;
        public DbSet<Gym.Data.Models.Cms.ParameterCategory> ParameterCategory { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships and constraints here if needed
            modelBuilder.Entity<Parameter>()
                .HasIndex(p => p.Name) // 
                .IsUnique();

            modelBuilder.Entity<PortalPage>()
                .HasIndex(p => p.Slug)
                .IsUnique();
        }
    }
}
