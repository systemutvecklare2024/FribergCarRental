using FribergCarRental.data;
using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

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

        public bool CarExist(int id)
        {
            return _carRepository.Any(x => x.Id == id);
        }

        public Contact GetContactFromUsername(string username)
        {
            var user = _userRepository.FindByUsernameWithContact(username);
            return user.Contact;
        }

        public User GetUserFromUsernameWithContact(string username)
        {
            return _userRepository.FindByUsernameWithContact(username);
        }

        public Booking CreateBooking(Car car, User user, DateTime start, DateTime end)
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

            var added = _bookingRepository.Add(booking);

            added.Car = _carRepository.Get(added.CarId);
            added.User = _userRepository.Get(added.UserId);

            return added;
        }

        public IEnumerable<BookingIndexViewModel> GetBookingsForUser(int? userId)
        {
            if (userId == null)
            {
                return Enumerable.Empty<BookingIndexViewModel>();
            }

            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            var bookings = _bookingRepository
                .Where(b => b.UserId == userId)
                .Select(b => new BookingIndexViewModel
                {
                    Id = b.Id,
                    CarModel = b.Car.Model,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    TotalCost = b.TotalCost,
                    IsUpcoming = b.StartDate > currentDate
                })
                .OrderBy( b => b.StartDate)
                .ToList();

            return bookings;
        }

        public Booking GetById(int bookingId)
        {
            var booking = _bookingRepository.Query()
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(b => b.Id == bookingId)
                .First();
            return booking;
        }

        public void RemoveBooking(Booking booking)
        {
            _bookingRepository.Remove(booking);
        }
    }
}
