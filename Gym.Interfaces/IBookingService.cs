using Gym.Data.Models.Core;

namespace Gym.Interfaces
{
    public enum BookingOperationStatus
    {
        Success,
        ClassNotFound,
        ClassFull,
        NoMember,
        BookingNotFound,
        AlreadyCancelled
    }

    public record BookingOperationResult(BookingOperationStatus Status);

    public interface IBookingService
    {
        Task<List<Booking>> GetActiveBookingsForCurrentMemberAsync();
        Task<Booking?> GetUpcomingBookingForCurrentMemberAsync();
        Task<BookingOperationResult> CreateBookingForCurrentMemberAsync(int fitnessClassId);
        Task<BookingOperationResult> CancelBookingAsync(int bookingId);
    }
}
