using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gym.Data.Models.Core;
using Gym.Data.Data;

namespace Gym.Admin.Controllers
{
    public class BookingsController : Controller
    {
        private readonly GymContext _context;

        public BookingsController(GymContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Booking.Include(b => b.FitnessClass).Include(b => b.Member);
            return View(await gymContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.FitnessClass)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["FitnessClassId"] = new SelectList(_context.Set<FitnessClass>(), "Id", "Room");
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Email");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FitnessClassId,Status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.IsActive = true;
                booking.CreatedAt = DateTime.UtcNow;
                booking.CreatedBy = User.Identity?.Name ?? "Admin";
                booking.ModifiedBy = User.Identity?.Name ?? "Admin";
                booking.ModifiedAt = DateTime.UtcNow;

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FitnessClassId"] = new SelectList(_context.Set<FitnessClass>(), "Id", "Room", booking.FitnessClassId);
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Email", booking.MemberId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            booking.ModifiedBy = User.Identity?.Name ?? "Admin";
            booking.ModifiedAt = DateTime.UtcNow;

            ViewData["FitnessClassId"] = new SelectList(_context.Set<FitnessClass>(), "Id", "Room", booking.FitnessClassId);
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Email", booking.MemberId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FitnessClassId,Status,Id,IsActive")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBooking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                    if (existingBooking == null)
                    {
                        return NotFound();
                    }

                    booking.CreatedBy = existingBooking.CreatedBy;
                    booking.CreatedAt = existingBooking.CreatedAt;
                    booking.DeletedBy = existingBooking.DeletedBy;
                    booking.DeletedAt = existingBooking.DeletedAt;
                    booking.ModifiedBy = User.Identity?.Name ?? "Admin";
                    booking.ModifiedAt = DateTime.UtcNow;

                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FitnessClassId"] = new SelectList(_context.Set<FitnessClass>(), "Id", "Room", booking.FitnessClassId);
            ViewData["MemberId"] = new SelectList(_context.Set<Member>(), "Id", "Email", booking.MemberId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.FitnessClass)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.DeletedAt = DateTime.UtcNow;
            booking.DeletedBy = User.Identity?.Name ?? "Admin";
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }
    }
}
