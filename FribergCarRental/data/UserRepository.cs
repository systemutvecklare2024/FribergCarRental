using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class UserRepository : GenericRepository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await dbContext
                .Set<User>()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<IEnumerable<User?>> AllWithBookingsAsync()
        {
            return await dbContext
                .Set<User>()
                .Include(u => u.Bookings)
                .ToListAsync();
        }

        public async Task<User?> GetWithBookingsAsync(int id)
        {
            return await dbContext
                .Set<User>()
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetAccount(string username)
        {
            return await dbContext
                .Set<User>()
                .Include(u => u.Contact)
                .FirstOrDefaultAsync(u => u.Username == username.ToLower());
        }
    }
}
