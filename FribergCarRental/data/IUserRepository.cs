using FribergCarRental.Models;

namespace FribergCarRental.data
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUsername(string username);
    }
}
