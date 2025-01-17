using FribergCarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rent()
        {
            return View();
        }
    }
}
