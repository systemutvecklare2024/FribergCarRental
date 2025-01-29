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
        public async Task<IActionResult> Index()
        {
            var userId = await _authService.GetCurrentUserId();
            var bookings = await _bookingService.GetBookingsForUser(userId);

            return View(bookings);
        }

        // GET: Booking/Create
        public async Task<IActionResult> Create(int carId)
        {
            if (!_authService.IsAuthenticated())
            {
                return RedirectToAction("LoginOrRegister", "Account", new { returnUrl = Url.Action("Create", "Booking", new { carId }) });
            }

            var car = await _carRepository.GetAsync(carId);
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
                CarModel = car.Model,
                CarPrice = car.PricePerDay,
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
        public async Task<IActionResult> Create(CreateBookingViewModel createBookingViewModel)
        {
            var user = await _authService.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction(
                    "LoginOrRegister", 
                    "Account", 
                    new { returnUrl = Url.Action("Create", "Booking", new { createBookingViewModel.CarId }) });
            }

            if (ModelState.IsValid)
            {
                var car = await _carRepository.GetAsync(createBookingViewModel.CarId);
                if (car == null)
                {
                    // Car not found, wtf do we do now?
                    return RedirectToAction("Index");
                }

                var booking = await _bookingService.CreateBookingAsync(
                    car,
                    user,
                    createBookingViewModel.StartDate,
                    createBookingViewModel.EndDate);

                if (booking == null)
                {
                    return NotFound();
                }

                return RedirectToAction("Confirmation", new { bookingId = booking.Id });
            }

            return View(createBookingViewModel);
        }

        // POST: Booking/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if(booking == null)
            {
                return NotFound("Kan ej hitta angiven bokning");
            }
            var currentUser = await _authService.GetCurrentUserId();

            if (booking.UserId != currentUser)
            {
                return Problem("Unauthorized");
            }

            await _bookingService.RemoveBookingAsync(booking);

            return RedirectToAction("Index");
        }

        // GET: Booking/Confirmation
        [SimpleAuthorize]
        public async Task<IActionResult> Confirmation(int bookingId)
        {
            var booking = await _bookingService.GetBookingByIdWithDetailAsync(bookingId);
            if(booking == null)
            {
                return NotFound();
            }

            if (booking.UserId != await _authService.GetCurrentUserId())
            {
                return RedirectToAction("Index");
            }

            return View(booking);
        }
    }
}
