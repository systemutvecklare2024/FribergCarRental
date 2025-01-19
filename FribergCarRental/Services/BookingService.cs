using FribergCarRental.data;
using FribergCarRental.Models.Entities;

namespace FribergCarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(ICarRepository carRepository, IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
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
    }
}
