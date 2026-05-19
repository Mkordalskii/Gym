using Gym.Data.Data;
using Gym.Interfaces;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Gym.Services
{
    public class BookingPdfService : IBookingPdfService
    {
        private readonly GymContext _context;
        public BookingPdfService(GymContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerateConfirmationPdfAsync(int bookingId)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var booking = await _context.Booking
                .Include(b => b.Member)
                .Include(b => b.FitnessClass)
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (booking == null)
                throw new Exception("Booking not found.");

            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);

                    page.Header()
                        .Text("Booking confirmation")
                        .FontSize(22)
                        .Bold();

                    page.Content().Column(column =>
                    {
                        column.Spacing(12);

                        column.Item().Text($"Booking ID: {booking.Id}");
                        column.Item().Text($"Member: {booking.Member.FirstName} {booking.Member.LastName}");
                        column.Item().Text($"Class: {booking.FitnessClass.Title}");
                        column.Item().Text($"Date: {booking.FitnessClass.StartTime:dd.MM.yyyy HH:mm}");
                        column.Item().Text($"Room: {booking.FitnessClass.Room}");
                        column.Item().Text($"Duration: {booking.FitnessClass.DurationInMinutes} min");
                        column.Item().Text($"Status: {booking.Status}");
                        column.Item().Text($"Generated at: {DateTime.Now:dd.MM.yyyy HH:mm}");
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Gym.Web - Customer Portal");
                });
            }).GeneratePdf();
        }
    }
}
