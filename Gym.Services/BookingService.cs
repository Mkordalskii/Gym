using Gym.Data.Data;
using Gym.Data.Models.Core;
using Gym.Interfaces;
using Gym.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class BookingService : BaseService, IBookingService
    {
        public BookingService(GymContext context) : base(context)
        {
        }

        public async Task<List<Booking>> GetActiveBookingsForCurrentMemberAsync()
        {
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return new List<Booking>();
            }

            return await _context.Booking
                .Include(b => b.FitnessClass)
                .Where(b => b.IsActive && b.MemberId == member.Id)
                .OrderByDescending(b => b.FitnessClass!.StartTime)
                .ToListAsync();
        }

        public async Task<Booking?> GetUpcomingBookingForCurrentMemberAsync()
        {
            // TEMP: brak logowania / fake user - bierzemy pierwszego (jedynego) membera
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return null;
            }

            var now = DateTime.Now;

            return await _context.Booking
                .Include(b => b.FitnessClass)
                .Where(b => b.IsActive
                            && b.MemberId == member.Id
                            && b.Status != "Cancelled"
                            && b.FitnessClass!.StartTime >= now)
                .OrderBy(b => b.FitnessClass!.StartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<BookingOperationResult> CreateBookingForCurrentMemberAsync(int fitnessClassId)
        {
            var fitnessClass = await _context.FitnessClass
                .Include(fc => fc.Bookings)
                .FirstOrDefaultAsync(fc => fc.Id == fitnessClassId);

            if (fitnessClass == null)
            {
                return new BookingOperationResult(BookingOperationStatus.ClassNotFound);
            }

            var activeBookings = fitnessClass.Bookings
                .Count(b => b.IsActive && b.Status != "Cancelled");

            if (activeBookings >= fitnessClass.Capacity)
            {
                return new BookingOperationResult(BookingOperationStatus.ClassFull);
            }

            // TEMP: brak logowania / fake user
            var member = await _context.Member.FirstOrDefaultAsync();

            if (member == null)
            {
                return new BookingOperationResult(BookingOperationStatus.NoMember);
            }

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

            return new BookingOperationResult(BookingOperationStatus.Success);
        }

        public async Task<BookingOperationResult> CancelBookingAsync(int bookingId)
        {
            var booking = await _context.Booking
                .Include(b => b.FitnessClass)
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.IsActive);

            if (booking == null)
            {
                return new BookingOperationResult(BookingOperationStatus.BookingNotFound);
            }

            if (booking.Status == "Cancelled")
            {
                return new BookingOperationResult(BookingOperationStatus.AlreadyCancelled);
            }

            booking.Status = "Cancelled";
            booking.ModifiedAt = DateTime.UtcNow;
            booking.ModifiedBy = "Web";

            await _context.SaveChangesAsync();

            return new BookingOperationResult(BookingOperationStatus.Success);
        }
    }
}
