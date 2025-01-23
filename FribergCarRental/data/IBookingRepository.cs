using FribergCarRental.Models.Entities;
using FribergCarRental.Models.ViewModel;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetAllWithDetailsAsync(Expression<Func<Booking, bool>> predicate);
        Task<IEnumerable<Booking>> GetAllWithDetailsAsync();
        Task<IEnumerable<Booking>> GetBookingsForUserAsync(int userId);
        Task<Booking?> GetByIdWithDetailAsync(int id);
    }
}
