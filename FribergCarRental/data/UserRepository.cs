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
            return await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
