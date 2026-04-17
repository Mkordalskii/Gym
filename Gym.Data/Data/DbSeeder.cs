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
                new Parameter { Name = "Home.PlanBasicName", Value = "Basic", Description = "Basic plan name", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanBasicDuration", Value = "30 days", Description = "Basic plan duration badge", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanBasicDescription", Value = "Perfect for beginners", Description = "Basic plan description", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanBasicPrice", Value = "129 PLN", Description = "Basic plan price", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanStandardName", Value = "Standard", Description = "Standard plan name", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanStandardBadge", Value = "Most popular", Description = "Standard plan badge", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanStandardDescription", Value = "The most frequently chosen", Description = "Standard plan description", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanStandardPrice", Value = "169 PLN", Description = "Standard plan price", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanPremiumName", Value = "Premium", Description = "Premium plan name", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanPremiumDuration", Value = "30 days", Description = "Premium plan duration badge", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanPremiumDescription", Value = "Full access + sauna", Description = "Premium plan description", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanPremiumPrice", Value = "219 PLN", Description = "Premium plan price", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanChooseButton", Value = "Choose plan", Description = "Choose plan button label", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new Parameter { Name = "Home.PlanHintText", Value = "Later: creates Membership", Description = "Hint under plan card", ParameterCategoryId = membershipPlansCategoryId, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
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
                new PortalPage { Slug = "home", Title = "Home", Content = "Main page", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new PortalPage { Slug = "schedule", Title = "Schedule", Content = "Class schedule page", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new PortalPage { Slug = "membership-plans", Title = "Membership plans", Content = "Membership plans page", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" },
                new PortalPage { Slug = "my-dashboard", Title = "My dashboard", Content = "Member dashboard page", IsPublished = true, IsActive = true, CreatedAt = now, CreatedBy = "Seeder", ModifiedAt = now, ModifiedBy = "Seeder" }
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
