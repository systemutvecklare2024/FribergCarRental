using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRental.data;
using FribergCarRental.Services;
using FribergCarRental.Filters;
using FribergCarRental.Areas.Admin.Models;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SimpleAuthorize(Role = "Admin")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;

        public BookingsController(IBookingService bookingService, ICarRepository carRepository, IUserRepository userRepository)
        {
            _bookingService = bookingService;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        // GET: Admin/Bookings
        public async Task<IActionResult> Index()
        {

            var bookings = await _bookingService.GetAllBookingsWithDetailsAsync();
            return View(bookings);
        }

        // GET: Admin/Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetBookingByIdWithDetailAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Admin/Bookings/Create
        public async Task<IActionResult> Create()
        {
            var modelCars = await _carRepository.AllAsync();
            var cars = modelCars?.Select(c => new
            {
                c.Id,
                c.Model,
                c.PricePerDay
            });

            var modelUsers = await _userRepository.AllAsync();
            var users = modelUsers?.Select(u => new
            {
                u.Id,
                u.Email
            });

            var viewModel = new AdminBookingViewModel
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime((DateTime.Now).AddDays(1)),
                Cars = cars?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Model }).ToList(),
                Users = users?.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Email }).ToList(),
                CarPrices = cars?.ToDictionary(c => c.Id, c => c.PricePerDay)
            };

            return View(viewModel);
        }

        // POST: Admin/Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminBookingViewModel booking)
        {
            if (ModelState.IsValid)
            {
                var car = await _carRepository.GetAsync(booking.CarId);
                var user = await _userRepository.GetAsync(booking.UserId);
                if (car == null || user == null)
                {
                    return NotFound();
                }

                var newBooking = await _bookingService
                    .CreateBookingAsync(
                        car,
                        user,
                        booking.StartDate.ToDateTime(TimeOnly.MinValue),
                        booking.EndDate.ToDateTime(TimeOnly.MinValue)
                    );

                return RedirectToAction(nameof(Index));
            }

            var modelCars = await _carRepository.AllAsync();
            var cars = modelCars?.Select(c => new
            {
                c.Id,
                c.Model,
                c.PricePerDay
            });

            var modelUsers = await _userRepository.AllAsync();
            var users = modelUsers?.Select(u => new
            {
                u.Id,
                u.Email
            });

            booking.CarPrices = cars?.ToDictionary(c => c.Id, c => c.PricePerDay);
            booking.Users = users?.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Email }).ToList();
            booking.Cars = cars?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Model }).ToList();

            return View(booking);
        }

        // GET: Admin/Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetByIdAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if(booking.StartDate < today)
            {
                return RedirectToAction("Index");
            }

            var modelCars = await _carRepository.AllAsync();
            var cars = modelCars?.Select(c => new
            {
                c.Id,
                c.Model,
                c.PricePerDay
            });

            var modelUsers = await _userRepository.AllAsync();
            var users = modelUsers?.Select(u => new
            {
                u.Id,
                u.Email
            });

            var viewModel = new AdminBookingViewModel
            {
                Id = id,
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime((DateTime.Now).AddDays(1)),
                Cars = cars?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Model }).ToList(),
                Users = users?.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Email }).ToList(),
                CarPrices = cars?.ToDictionary(c => c.Id, c => c.PricePerDay)
            };

            return View(viewModel);
        }

        // POST: Admin/Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,UserId,StartDate,EndDate")] AdminBookingViewModel booking)
        {
            if (id != booking.Id || !await BookingExists(booking.Id.Value))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(booking);
            }

            try
            {
                await _bookingService.UpdateBookingAsync(booking);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem("Concurrency problem");
            }
        }

        // GET: Admin/Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetBookingByIdWithDetailAsync(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Admin/Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking != null)
            {
                await _bookingService.RemoveBookingAsync(booking);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookingExists(int id)
        {
            return await _bookingService.GetByIdAsync(id) != null;
        }
    }
}
