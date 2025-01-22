using FribergCarRental.Models.Entities;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> Where(Expression<Func<Booking, bool>> predicate);
    }
}
