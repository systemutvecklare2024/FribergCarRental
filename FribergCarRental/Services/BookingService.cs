using FribergCarRental.data;
using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;

namespace FribergCarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(ICarRepository carRepository, IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<IEnumerable<BookingIndexViewModel>> GetBookingsForUser(int? userId)
        {
            if (userId == null)
            {
                return [];
            }

            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            var bookings = await _bookingRepository.GetBookingsForUserAsync(userId.Value);

            var bookingViewModels = bookings.Select(b => new BookingIndexViewModel
            {
                Id = b.Id,
                CarModel = b?.Car?.Model ?? "Unknown",
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                TotalCost = b.TotalCost,
                IsUpcoming = b.StartDate > currentDate
            })
            .OrderBy(b => b.StartDate)
            .ToList();

            return bookingViewModels;
        }

        public async Task RemoveBooking(Booking booking)
        {
            await _bookingRepository.RemoveAsync(booking);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsWithDetailsAsync()
        {
            return await _bookingRepository.GetAllWithDetailsAsync();
        }

        public async Task<Booking?> GetBookingByIdWithDetailAsync(int id)
        {
            return await _bookingRepository.GetByIdWithDetailAsync(id);
        }

        public async Task UpdateBookingAsync(Booking updatedBooking)
        {
            await _bookingRepository.UpdateAsync(updatedBooking);

            return;
        }
        public async Task<Booking?> CreateBookingAsync(Car car, User user, DateTime start, DateTime end)
        {
            var startDate = DateOnly.FromDateTime(start);
            var endDate = DateOnly.FromDateTime(end);

            var timeSpan = (end - start).Days;
            var totalCost = car.PricePerDay * timeSpan;

            var booking = new Booking
            {
                CarId = car.Id,
                UserId = user.Id,
                StartDate = startDate,
                EndDate = endDate,
                TotalCost = totalCost
            };

            var added = await _bookingRepository.AddAsync(booking);
            if (added != null)
            {
                added.Car = await _carRepository.GetAsync(added.CarId);
                added.User = await _userRepository.GetAsync(added.UserId);
            }

            return added;
        }
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetAsync(id);
        }

        public async Task RemoveBookingAsync(Booking booking)
        {
            await _bookingRepository.RemoveAsync(booking);
        }
    }
}
