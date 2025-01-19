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

        public BookingController(IBookingService bookingService, IAuthService authService)
        {
            _bookingService = bookingService;
            _authService = authService;
        }

        // GET: Booking/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Booking/Create
        public IActionResult Create(int carId)
        {
            if (!_authService.IsAuthenticated())
            {
                return RedirectToAction("LoginOrRegister", "Account", new { returnUrl = Url.Action("Create", "Booking", new { carId }) });
            }

            if (!_bookingService.CarExist(carId))
            {
                return NotFound("Bil hittades ej");
            }

            var username = _authService.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Kund hittades ej");
            }

            var contact = _bookingService.GetContactFromUsername(username);
            if (contact == null)
            {
                return NotFound("Kund hittades ej");
            }

            var model = new CreateBookingViewModel
            {
                CarId = carId,
                ContactId = contact.Id,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            model.StartDateString = model.StartDate.ToString("yyyy-MM-dd");
            model.EndDateString = model.EndDate.ToString("yyyy-MM-dd");


            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SimpleAuthorize]
        public IActionResult Create(CreateBookingViewModel createBookingViewModel)
        {
            if (ModelState.IsValid)
            {

            }
            return View(createBookingViewModel);
        }
    }
}
