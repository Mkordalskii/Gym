using Gym.Data.Data;
using Gym.Interfaces;
using Gym.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GymContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymContext") ?? throw new InvalidOperationException("Connection string 'GymContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IParameterService, ParameterService>();
builder.Services.AddScoped<IPortalPageService, PortalPageService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFitnessClassService, FitnessClassService>();
builder.Services.AddScoped<IMembershipPlanService, MembershipPlanService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GymContext>();
    await DbSeeder.SeedAsync(context);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{slug?}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
