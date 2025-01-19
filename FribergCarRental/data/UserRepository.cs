using FribergCarRental.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRental.data
{
    public class UserRepository : GenericRepository<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User FindByUsername(string username)
        {
            return Find(x => x.Username == username).FirstOrDefault();
        }

        public User FindByUsernameWithContact(string username)
        {
            return dbContext.Set<User>().Where(x => x.Username == username).Include(x => x.Contact).FirstOrDefault();

        }
    }
}
