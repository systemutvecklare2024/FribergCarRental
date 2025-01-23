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

        public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await dbContext
                .Set<Booking>()
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync()
        {
            return await dbContext
                .Set<Booking>()
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(b => true)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdWithDetailAsync(int id)
        {
            return await dbContext
                .Set<Booking>()
                .Include(b => b.Car)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsForUserAsync(int userId)
        {
            return await dbContext.Set<Booking>()
                .Include(b => b.Car)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
