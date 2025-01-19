using FribergCarRental.Models.Entities;

namespace FribergCarRental.Services
{
    public interface IBookingService
    {
        IEnumerable<Car> GetAllCars();
    }
}
