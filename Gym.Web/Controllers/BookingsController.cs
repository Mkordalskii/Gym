using Gym.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IParameterService _parameterService;
        private readonly IPortalPageService _portalPageService;
        public BookingsController(IBookingService bookingService, IParameterService parameterService, IPortalPageService portalPageService)
        {
            _bookingService = bookingService;
            _parameterService = parameterService;
            _portalPageService = portalPageService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PageModel = await _portalPageService.GetPublishedPortalPagesAsync();
            ViewBag.Parameters = await _parameterService.GetAllActiveParametersAsync();

            var bookings = await _bookingService.GetActiveBookingsForCurrentMemberAsync();

            return View(bookings);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int fitnessClassId)
        {
            var result = await _bookingService.CreateBookingForCurrentMemberAsync(fitnessClassId);

            switch (result.Status)
            {
                case BookingOperationStatus.ClassNotFound:
                    return NotFound();
                case BookingOperationStatus.ClassFull:
                    TempData["Error"] = "Class is full.";
                    return RedirectToAction("Index", "FitnessClasses");
                case BookingOperationStatus.NoMember:
                    return BadRequest("No member");
                case BookingOperationStatus.Success:
                    TempData["Success"] = "Booking created.";
                    return RedirectToAction("Index", "FitnessClasses");
                default:
                    return RedirectToAction("Index", "FitnessClasses");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var result = await _bookingService.CancelBookingAsync(id);

            switch (result.Status)
            {
                case BookingOperationStatus.BookingNotFound:
                    return NotFound();
                case BookingOperationStatus.AlreadyCancelled:
                    TempData["Error"] = "Booking is already cancelled.";
                    return RedirectToAction(nameof(Index));
                case BookingOperationStatus.Success:
                    TempData["Success"] = "Booking cancelled.";
                    return RedirectToAction(nameof(Index));
                default:
                    return RedirectToAction(nameof(Index));
            }
        }
    }
}
