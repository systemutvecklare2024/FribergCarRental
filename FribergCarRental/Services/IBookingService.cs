using FribergCarRental.Models;

namespace FribergCarRental.Services
{
    public interface IBookingService
    {
        IEnumerable<Car> GetAllCars();
    }
}
