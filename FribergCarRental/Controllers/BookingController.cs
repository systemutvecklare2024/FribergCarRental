using FribergCarRental.data;
using FribergCarRental.Filters;
using FribergCarRental.Models.ViewModel;
using FribergCarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IAuthService _authService;
        private readonly ICarRepository _carRepository;

        public BookingController(IBookingService bookingService, IAuthService authService, ICarRepository carRepository)
        {
            _bookingService = bookingService;
            _authService = authService;
            _carRepository = carRepository;
        }

        // GET: Booking/Index
        public IActionResult Index()
        {
            var userId = _authService.GetCurrentUser()?.Id;
            var bookings = _bookingService.GetBookingsForUser(userId);

            return View(bookings);
        }

        // GET: Booking/Create
        public IActionResult Create(int carId)
        {
            if (!_authService.IsAuthenticated())
            {
                return RedirectToAction("LoginOrRegister", "Account", new { returnUrl = Url.Action("Create", "Booking", new { carId }) });
            }

            var car = _carRepository.Get(carId);
            if (car == null)
            {
                return NotFound("Bil hittades ej");
            }

            var username = _authService.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Kund hittades ej");
            }

            var model = new CreateBookingViewModel
            {
                CarId = carId,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize]
        public IActionResult Create(CreateBookingViewModel createBookingViewModel)
        {
            var user = _authService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(
                    "LoginOrRegister", 
                    "Account", 
                    new { returnUrl = Url.Action("Create", "Booking", new { createBookingViewModel.CarId }) });
            }

            if (ModelState.IsValid)
            {
                var car = _carRepository.Get(createBookingViewModel.CarId);
                if (car == null)
                {
                    // Car not found, wtf do we do now?
                    return RedirectToAction("Index");
                }

                var booking = _bookingService.CreateBooking(
                    car,
                    user,
                    createBookingViewModel.StartDate,
                    createBookingViewModel.EndDate);

                return RedirectToAction("Confirmation", new { bookingId = booking.Id });
            }

            return View(createBookingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var booking = _bookingService.GetById(id);
            if(booking == null)
            {
                return NotFound("Kan ej hitta angiven bokning");
            }

            _bookingService.RemoveBooking(booking);

            return RedirectToAction("Index");
        }

        [SimpleAuthorize]
        public IActionResult Confirmation(int bookingId)
        {
            var booking = _bookingService.GetById(bookingId);
            if (booking.UserId != _authService.GetCurrentUserId())
            {
                return RedirectToAction("Index ");
            }
            return View(booking);
        }
    }
}
