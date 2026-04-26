using Gym.Data.Models.Cms;
using Gym.Data.Models.Core;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(GymContext context)
        {
            await context.Database.MigrateAsync();

            await SeedParameterCategoriesAsync(context);
            await SeedParametersAsync(context);
            await SeedMembershipPlansAsync(context);
            await SeedAnnouncementsAsync(context);
            await SeedPortalPagesAsync(context);
        }

        private static async Task SeedParameterCategoriesAsync(GymContext context)
        {
            var now = DateTime.UtcNow;

            var categories = new[]
            {
                new ParameterCategory { Name = "HomePage", Description = "Texts used on the home page", IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new ParameterCategory { Name = "Footer", Description = "Texts used in the site footer", IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new ParameterCategory { Name = "MembershipPlans", Description = "Texts used in membership plans section", IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new ParameterCategory { Name = "Dashboard", Description = "Texts used in dashboard cards", IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" }
            };

            foreach (var category in categories)
            {
                if (!await context.ParameterCategory.AnyAsync(c => c.Name == category.Name))
                {
                    context.ParameterCategory.Add(category);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedParametersAsync(GymContext context)
        {
            var categories = await context.ParameterCategory
                .AsNoTracking()
                .ToDictionaryAsync(c => c.Name, c => c.Id);

            if (!categories.TryGetValue("HomePage", out var homePageCategoryId) ||
                !categories.TryGetValue("Footer", out var footerCategoryId) ||
                !categories.TryGetValue("MembershipPlans", out var membershipPlansCategoryId) ||
                !categories.TryGetValue("Dashboard", out var dashboardCategoryId))
            {
                return;
            }

            var now = DateTime.UtcNow;

            var parameters = new[]
            {
                // gym.web home page
                new Parameter { Name = "Home.PageTitle", Value = "Customer web", Description = "Page title for home view", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroTitle", Value = "Your fitness club portal", Description = "Main hero title", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroSubtitle", Value = "Manage your membership, browse the schedule, and book classes in seconds.", Description = "Main hero subtitle", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroPrimaryButton", Value = "View schedule", Description = "Primary hero button label", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroSecondaryButton", Value = "Membership plans", Description = "Secondary hero button label", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroTertiaryButton", Value = "My dashboard", Description = "Tertiary hero button label", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.HeroTip", Value = "Tip: booking requires an active membership.", Description = "Hero tip text", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClassesTitle", Value = "Next classes (today)", Description = "Card title with next classes", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClassesBadge", Value = "Live", Description = "Badge in next classes card", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass1Name", Value = "Yoga Flow", Description = "First next class name", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass1Time", Value = "18:00", Description = "First next class time", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass2Name", Value = "Crossfit Intro", Description = "Second next class name", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass2Time", Value = "19:30", Description = "Second next class time", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass3Name", Value = "Pilates Core", Description = "Third next class name", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClass3Time", Value = "20:15", Description = "Third next class time", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.NextClassesButton", Value = "Book a class", Description = "Button label under classes list", ParameterCategoryId = homePageCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.MembershipPlansTitle", Value = "Membership plans", Description = "Membership plans section title", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.MembershipPlansEntityLabel", Value = "Entity: MembershipPlan", Description = "Membership plans section entity label", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipLabel", Value = "Active membership", Description = "Dashboard first card header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipPlanName", Value = "Premium 30 days", Description = "Active membership plan name", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipStatusLabel", Value = "Status", Description = "Active membership status label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipStatusValue", Value = "Active", Description = "Active membership status value", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipExpiresLabel", Value = "Expires on", Description = "Active membership expiration label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipExpiresValue", Value = "24.04.2026", Description = "Active membership expiration value", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.ActiveMembershipEntityLabel", Value = "Entity: Membership + MembershipPlan", Description = "Active membership entity label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingLabel", Value = "Upcoming booking", Description = "Dashboard second card header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingClassName", Value = "HIIT Cardio", Description = "Upcoming booking class name", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingStatus", Value = "Booked", Description = "Upcoming booking status", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingWhen", Value = "Tomorrow, 17:45", Description = "Upcoming booking date and time", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingRoomLabel", Value = "Room", Description = "Upcoming booking room label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingRoomValue", Value = "B", Description = "Upcoming booking room value", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingDurationLabel", Value = "Duration", Description = "Upcoming booking duration label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingDurationValue", Value = "45 min", Description = "Upcoming booking duration value", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.UpcomingBookingEntityLabel", Value = "Entity: Booking + FitnessClass", Description = "Upcoming booking entity label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanDefaultDescription", Value = "Membership plan", Description = "Fallback description for plans", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanChooseButton", Value = "Choose plan", Description = "Choose plan button label", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanHintText", Value = "Later: creates Membership", Description = "Hint under plan card", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                // Admin dashboard
                new Parameter { Name = "Admin.Dashboard.PageTitle", Value = "Dashboard", Description = "Admin dashboard page title", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.ActiveMembers", Value = "Members (active)", Description = "Active members card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.TotalMembers", Value = "Total members", Description = "Total members card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.ActiveMemberships", Value = "Active memberships", Description = "Active memberships card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.ExpiringSoon", Value = "Expiring in 7 days", Description = "Expiring memberships card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.TodayBookings", Value = "Bookings (today)", Description = "Today bookings card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.TodayCancellations", Value = "Cancellations today", Description = "Today cancellations card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.CmsContent", Value = "CMS content", Description = "CMS content card label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Pages", Value = "Pages", Description = "CMS pages label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.UpcomingClasses", Value = "Upcoming classes", Description = "Upcoming classes panel title", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Today", Value = "Today", Description = "Today badge label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Class", Value = "Class", Description = "Class table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Start", Value = "Start", Description = "Start table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Room", Value = "Room", Description = "Room table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Capacity", Value = "Capacity", Description = "Capacity table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Bookings", Value = "Bookings", Description = "Bookings table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Status", Value = "Status", Description = "Status table header", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Full", Value = "Full", Description = "Full class status", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.Open", Value = "Open", Description = "Open class status", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.NoClassesToday", Value = "No classes scheduled for today.", Description = "Empty classes table text", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.CapacityTip", Value = "Tip: capacity is based on FitnessClass.Capacity and bookings count.", Description = "Capacity explanation text", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.ViewFullSchedule", Value = "View full schedule", Description = "Full schedule button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.QuickActions", Value = "Quick actions", Description = "Quick actions title", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.AddMember", Value = "Add member", Description = "Add member button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.CreateMembershipPlan", Value = "Create membership plan", Description = "Create membership plan button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.AddClass", Value = "Add class to schedule", Description = "Add class button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.PostAnnouncement", Value = "Post announcement", Description = "Post announcement button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.EditParameters", Value = "Edit parameters (CMS)", Description = "Edit parameters button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Admin.Dashboard.EditPortalPages", Value = "Edit portal pages (CMS)", Description = "Edit portal pages button label", ParameterCategoryId = dashboardCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                // footer
                new Parameter { Name = "Footer.Copyright", Value = "2026 Gym.Web - Customer Web Portal", Description = "Footer copyright text", ParameterCategoryId = footerCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Footer.PrivacyPolicy", Value = "Privacy policy", Description = "Footer privacy policy link label", ParameterCategoryId = footerCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" }
            };

            foreach (var parameter in parameters)
            {
                if (!await context.Parameter.AnyAsync(p => p.Name == parameter.Name))
                {
                    context.Parameter.Add(parameter);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedMembershipPlansAsync(GymContext context)
        {
            var now = DateTime.UtcNow;

            var plans = new[]
            {
                new MembershipPlan { Name = "Basic", DurationInDays = 30, Price = 129.00m, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new MembershipPlan { Name = "Standard", DurationInDays = 30, Price = 169.00m, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new MembershipPlan { Name = "Premium", DurationInDays = 30, Price = 219.00m, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" }
            };

            foreach (var plan in plans)
            {
                if (!await context.MembershipPlan.AnyAsync(p => p.Name == plan.Name))
                {
                    context.MembershipPlan.Add(plan);
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedAnnouncementsAsync(GymContext context)
        {
            if (await context.Announcement.AnyAsync())
            {
                return;
            }

            var now = DateTime.UtcNow;

            context.Announcement.Add(
                new Announcement
                {
                    Title = "Welcome to Gym.Web",
                    Content = "Check the latest classes and book your training directly from the homepage.",
                    PublishFrom = now,
                    PublishTo = now.AddMonths(3),
                    IsActive = true,
                    CreatedAt = now,
                    CreatedBy = "Seeder",
                    ModifiedAt = now,
                    ModifiedBy = "Seeder"
                });

            await context.SaveChangesAsync();
        }

        private static async Task SeedPortalPagesAsync(GymContext context)
        {
            var now = DateTime.UtcNow;

            var pages = new[]
            {
                new PortalPage { Slug = "about", Title = "About", Content = "<p>Gym.Web is your fitness club portal where members can browse schedules, choose membership plans, and manage bookings.</p>", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new PortalPage { Slug = "terms", Title = "Terms", Content = "<p>By using Gym.Web, you agree to the terms of service and club regulations available on this page.</p>", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" }
            };

            foreach (var page in pages)
            {
                if (!await context.PortalPage.AnyAsync(p => p.Slug == page.Slug))
                {
                    context.PortalPage.Add(page);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
