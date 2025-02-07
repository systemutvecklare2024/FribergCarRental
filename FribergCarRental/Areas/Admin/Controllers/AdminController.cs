using FribergCarRental.Areas.Admin.Models;
using FribergCarRental.data;
using FribergCarRental.Filters;
using FribergCarRental.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ICarRepository _carRepository;
        private readonly IAuthService _authService;

        public AdminController(IUserRepository userRepository, IBookingRepository bookingRepository, ICarRepository carRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
            _authService = authService;
        }
        //GET: Admin
        public async Task<IActionResult> Index()
        {
            if (!_authService.IsAuthenticated() && !await _authService.IsAdmin())
            {
                return RedirectToAction("LoginOrRegister", "Account", new { 
                    area = "",
                    returnUrl = Url.Action("Index", "Admin", new { area = "Admin"} ) });
            }

            return RedirectToAction("Dashboard", "Admin");
            
        }

        // Get: Admin/Dashboard
        [SimpleAuthorize( Role = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var users = await _userRepository.AllAsync() ?? [];
            var cars = await _carRepository.AllAsync() ?? [];
            var bookings = await _bookingRepository.AllAsync() ?? [];

            var dashboard = new AdminDashboardViewModel()
            {
                BookingsCount = bookings.Count(),
                CarsCount = cars.Count(),
                UsersCount = users.Count(),
            };

            return View(dashboard);
        }
    }
}
