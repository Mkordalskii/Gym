namespace Gym.Interfaces
{
    public interface IBookingPdfService
    {
        Task<byte[]> GenerateConfirmationPdfAsync(int bookingId);
    }
}
