using FribergCarRental.data;
using FribergCarRental.Models;

namespace FribergCarRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICarRepository _carRepository;

        public BookingService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _carRepository.All();
        }
    }
}
