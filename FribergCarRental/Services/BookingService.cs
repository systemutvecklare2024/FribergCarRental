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

            return added;
        }
    }
}
