using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class CarRepository : GenericRepository<Car, ApplicationDbContext>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Car>?> AllWithBookingsAsync()
        {
            return await dbContext
                .Set<Car>()
                .Include(c => c.Bookings)
                .ToListAsync();
        }

        public async Task<Car?> GetWithBookingsAsync(int id)
        {
            return await dbContext
                .Set<Car>()
                .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
