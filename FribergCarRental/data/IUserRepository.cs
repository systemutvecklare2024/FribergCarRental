using FribergCarRental.Models.Entities;

namespace FribergCarRental.data
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUsername(string username);
        User FindByUsernameWithContact(string username);
    }
}
