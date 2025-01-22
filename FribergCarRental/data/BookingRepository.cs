using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRental.data
{
    public class BookingRepository : GenericRepository<Booking, ApplicationDbContext>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Booking> Where(Expression<Func<Booking, bool>> predicate)
        {
            return dbContext.Bookings
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(predicate)
                .ToList();
        }
    }
}
