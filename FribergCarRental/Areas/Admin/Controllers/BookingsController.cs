using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FribergCarRental.Models.Entities;
using FribergCarRental.data;
using FribergCarRental.Services;

namespace FribergCarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            ViewData["CarId"] = new SelectList(await _carRepository.AllAsync(), "Id", "ImageUrl");
            ViewData["UserId"] = new SelectList(await _userRepository.AllAsync(), "Id", "Email");
            return View();
        }

        // POST: Admin/Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,UserId,StartDate,EndDate,TotalCost")] Booking booking)
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

            ViewData["CarId"] = new SelectList(await _carRepository.AllAsync(), "Id", "Model", booking.CarId);
            ViewData["UserId"] = new SelectList(await _userRepository.AllAsync(), "Id", "Email", booking.UserId);
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

            ViewData["CarId"] = new SelectList(await _carRepository.AllAsync(), "Id", "Model", booking.CarId);
            ViewData["UserId"] = new SelectList(await _userRepository.AllAsync(), "Id", "Email", booking.UserId);
            
            return View(booking);
        }

        // POST: Admin/Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,UserId,StartDate,EndDate,TotalCost")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBooking = await _bookingService.GetByIdAsync(booking.Id);
                    if (updatedBooking == null)
                    {
                        return NotFound();
                    }

                    // Update fields
                    updatedBooking.CarId = booking.CarId;
                    updatedBooking.UserId = booking.UserId;
                    updatedBooking.StartDate = booking.StartDate;
                    updatedBooking.EndDate = booking.EndDate;
                    updatedBooking.TotalCost = booking.TotalCost;

                    await _bookingService.UpdateBookingAsync(updatedBooking);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(await _carRepository.AllAsync(), "Id", "Model", booking.CarId);
            ViewData["UserId"] = new SelectList(await _userRepository.AllAsync(), "Id", "Email", booking.UserId);
            return View(booking);
        }

        // GET: Admin/Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
