using FribergCarRental.Areas.Admin.Models;
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

        /// <summary>
        /// Retrieves all bookings for a specified user and maps them to a collection of <see cref="BookingIndexViewModel"/>.
        /// </summary>
        /// <param name="userId">
        /// The ID of the user for whom the bookings are to be retrieved. If <c>null</c>, an empty collection is returned.
        /// </param>
        /// <returns>
        /// A collection of <see cref="BookingIndexViewModel"/> representing the user's bookings, 
        /// ordered by start date. If no user ID is provided, returns an empty collection.
        /// </returns>
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

        /// <summary>
        /// Removes the specified booking from the database.
        /// </summary>
        /// <param name="booking">
        /// The <see cref="Booking"/> entity to be removed.
        /// </param>
        public async Task RemoveBooking(Booking booking)
        {
            await _bookingRepository.RemoveAsync(booking);
        }

        /// <summary>
        /// Retrieves all bookings along with their related details from the database.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="Booking"/> entities with associated details.
        /// </returns>
        public async Task<IEnumerable<Booking>> GetAllBookingsWithDetailsAsync()
        {
            return await _bookingRepository.GetAllWithDetailsAsync();
        }

        /// <summary>
        /// Retrieves a booking by its ID along with its related details from the database.
        /// </summary>
        /// <param name="id">
        /// The ID of the booking to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Booking"/> entity with its associated details, or <c>null</c> if no booking is found with the specified ID.
        /// </returns>
        public async Task<Booking?> GetBookingByIdWithDetailAsync(int id)
        {
            return await _bookingRepository.GetByIdWithDetailAsync(id);
        }

        /// <summary>
        /// Updates the details of an existing booking in the database.
        /// </summary>
        /// <param name="updatedBooking">
        /// The <see cref="Booking"/> entity containing the updated booking details.
        /// </param>
        public async Task UpdateBookingAsync(Booking updatedBooking)
        {
            await _bookingRepository.UpdateAsync(updatedBooking);
        }

        public async Task UpdateBookingAsync(AdminBookingViewModel booking)
        {
            try
            {
                var existing = await _bookingRepository.GetAsync(booking.Id.Value) ?? throw new KeyNotFoundException($"Det finns ingen bokning med ID {booking.Id.Value}");
                var car = await _carRepository.GetAsync(booking.CarId) ?? throw new KeyNotFoundException($"Det finns ingen bil med ID {booking.CarId}");

                // Update fields
                existing.StartDate = booking.StartDate;
                existing.EndDate = booking.EndDate;
                existing.CarId = booking.CarId;
                existing.UserId = booking.UserId;
                existing.TotalCost = CalculateCost(
                    car.PricePerDay, 
                    booking.StartDate.ToDateTime(TimeOnly.MinValue), 
                    booking.EndDate.ToDateTime(TimeOnly.MinValue));

                await _bookingRepository.UpdateAsync(existing);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new booking for a user and a car, calculating the total cost based on the rental duration.
        /// </summary>
        /// <param name="car">
        /// The <see cref="Car"/> to be rented.
        /// </param>
        /// <param name="user">
        /// The <see cref="User"/> making the booking.
        /// </param>
        /// <param name="start">
        /// The start date of the booking.
        /// </param>
        /// <param name="end">
        /// The end date of the booking.
        /// </param>
        /// <returns>
        /// A <see cref="Booking"/> entity representing the newly created booking, or <c>null</c> if the creation fails.
        /// </returns>
        public async Task<Booking?> CreateBookingAsync(Car car, User user, DateTime start, DateTime end)
        {
            var startDate = DateOnly.FromDateTime(start);
            var endDate = DateOnly.FromDateTime(end);

            decimal totalCost = CalculateCost(car.PricePerDay, start, end);

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

        private static decimal CalculateCost(decimal pricePerDay, DateTime start, DateTime end)
        {
            
            var timeSpan = (end - start).Days;
            return pricePerDay * timeSpan;
        }

        /// <summary>
        /// Retrieves a booking by its ID from the database.
        /// </summary>
        /// <param name="id">
        /// The ID of the booking to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Booking"/> entity if found, or <c>null</c> if no booking exists with the specified ID.
        /// </returns>
        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _bookingRepository.GetAsync(id);
        }

        /// <summary>
        /// Removes the specified booking from the database.
        /// </summary>
        /// <param name="booking">
        /// The <see cref="Booking"/> entity to be removed.
        /// </param>
        public async Task RemoveBookingAsync(Booking booking)
        {
            await _bookingRepository.RemoveAsync(booking);
        }
    }
}
