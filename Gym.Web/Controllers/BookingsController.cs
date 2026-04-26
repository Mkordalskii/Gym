using Gym.Data.Data;
using Gym.Data.Models.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly GymContext _context;
        public BookingsController(GymContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _context.PortalPage
                .Where(p => p.IsPublished)
                .OrderBy(p => p.Id)
                .ToListAsync();

            ViewBag.Parameters = await _context.Parameter
                .Where(p => p.IsActive)
                .ToDictionaryAsync(p => p.Name, p => p.Value);

            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return View(new List<Gym.Data.Models.Core.Booking>());
            }

            var bookings = await _context.Booking
                .Include(b => b.FitnessClass)
                .Where(b => b.IsActive && b.MemberId == member.Id)
                .OrderByDescending(b => b.FitnessClass.StartTime)
                .ToListAsync();

            return View(bookings);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int fitnessClassId)
        {
            var fitnessClass = await _context.FitnessClass
                .Include(fc => fc.Bookings)
                .FirstOrDefaultAsync(fc => fc.Id == fitnessClassId);

            if (fitnessClass == null)
                return NotFound();

            var activeBookings = fitnessClass.Bookings
                .Count(b => b.IsActive && b.Status != "Cancelled");

            if (activeBookings >= fitnessClass.Capacity)
            {
                TempData["Error"] = "Class is full.";
                return RedirectToAction("Index", "FitnessClasses");
            }

            // TEMP: brak logowania / fake user
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
                return BadRequest("No member");

            var booking = new Booking
            {
                MemberId = member.Id,
                FitnessClassId = fitnessClassId,
                Status = "Booked",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Web"
            };

            _context.Booking.Add(booking);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking created.";

            return RedirectToAction("Index", "FitnessClasses");
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _context.Booking
                .Include(b => b.FitnessClass)
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);

            if (booking == null)
                return NotFound();

            if (booking.Status == "Cancelled")
            {
                TempData["Error"] = "Booking is already cancelled.";
                return RedirectToAction(nameof(Index));
            }

            booking.Status = "Cancelled";
            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedBy = "Web";

            await _context.SaveChangesAsync();

            TempData["Success"] = "Booking cancelled.";
            return RedirectToAction(nameof(Index));
        }
    }
}
