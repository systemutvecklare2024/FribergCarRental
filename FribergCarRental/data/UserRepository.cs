using FribergCarRental.Models;

namespace FribergCarRental.data
{
    public class UserRepository : GenericRepository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
