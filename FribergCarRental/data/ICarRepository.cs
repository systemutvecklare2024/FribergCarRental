using FribergCarRental.Models.Entities;
using System.Collections;

namespace FribergCarRental.data
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>?> AllWithBookingsAsync();
        Task<Car?> GetWithBookingsAsync(int id);
    }
}
